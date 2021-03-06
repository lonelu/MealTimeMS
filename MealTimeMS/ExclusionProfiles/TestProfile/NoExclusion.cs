using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealTimeMS.Data;
using MealTimeMS.Data.Graph;
using MealTimeMS.Util;
using MealTimeMS.IO;

namespace MealTimeMS.ExclusionProfiles.TestProfile
{
	public class NoExclusion : ExclusionProfile
	{
		static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
		public Dictionary<String, double> rtCalcPredictedRT;
		public List<ObservedPeptideRtTrackerObject> peptideIDRT;
		public NoExclusion(Database _database, double _retentionTimeWindowSize) : base(_database)
		{
			rtCalcPredictedRT = new Dictionary<string, double>();
			peptideIDRT = new List<ObservedPeptideRtTrackerObject>();
			setRetentionTimeWindow(_retentionTimeWindowSize);

		}

		override
		protected void evaluateIdentification(IDs id)
		{
			log.Debug("NoExclusion. Scores added, but nothing added to the exclusion list");

			// check if the peptide is identified or not
			if (id == null)
			{
				performanceEvaluator.countMS2UnidentifiedAnalyzed();
				return;
			}

			Peptide pep = getPeptideFromIdentification(id); // if it was going to be null, it already returned
															// is fragmented

			// add decoy or non-existent protein connections
			// database.addProteinFromIdentification(pep, id.getParentProteinAccessions());

			Double xCorr = id.getXCorr();
			Double dCN = id.getDeltaCN();
			pep.addScore(xCorr, 0.0, dCN);
			performanceEvaluator.evaluateAnalysis(exclusionList, pep);



			RetentionTime rt = pep.getRetentionTime();

			if (!rtCalcPredictedRT.Keys.Contains(pep.getSequence()))
			{
				rtCalcPredictedRT.Add(pep.getSequence(), rt.getRetentionTimePeak());
			}
		
			ObservedPeptideRtTrackerObject observedPep = new ObservedPeptideRtTrackerObject(pep.getSequence(), id.getScanTime(), id.getXCorr(), 
				rt.getRetentionTimePeak(), rt.getRetentionTimeStart() + GlobalVar.retentionTimeWindowSize, 
				RetentionTime.getRetentionTimeOffset(), rtCalcPredictedRT[pep.getSequence()], (rt.IsPredicted() ? 1 : 0));



			if ((xCorr > 2.5))
			{
				// calibrates our retention time alignment if the observed time is different
				// from the predicted only if it passes this threshold
				calibrateRetentionTime(pep);
			}
			observedPep.offset = RetentionTime.getRetentionTimeOffset();
			peptideIDRT.Add(observedPep);
		}


		override
		public String ToString()
		{
			double retentionTimeWindow = database.getRetentionTimeWindow();
			double ppmTolerance = exclusionList.getPPMTolerance();
			return "NoExclusion[" + "RT_window: " + retentionTimeWindow + ";ppmTolerance: " + ppmTolerance + "]";
		}

		override
		public ExclusionProfileEnum getAnalysisType()
		{
			return ExclusionProfileEnum.NO_EXCLUSION_PROFILE;
		}


	}
	public class ObservedPeptideRtTrackerObject{
		public String peptideSequence;
		public double arrivalTime;
		public double xcorr;
		public double rtPeak;
		public double correctedRT;
		public double offset;
		public double originalRTCalcPredictedValue;
		public int isPredicted;

		public ObservedPeptideRtTrackerObject(String _peptideSequence, double _arrivalTime, double _xcorr, double _rtPeak, double _correctedRT, double _offset, double _originalRTCalcPredictedValue, int _isPredicted)
		{
			peptideSequence = _peptideSequence;
			arrivalTime = _arrivalTime;
			xcorr = _xcorr;
			rtPeak = _rtPeak;
			correctedRT = _correctedRT;
			offset = _offset;
			originalRTCalcPredictedValue = _originalRTCalcPredictedValue;
			isPredicted = _isPredicted;
		}
		public ObservedPeptideRtTrackerObject() {
		}

		override
		public String ToString()
		{
			String str = String.Join("\t", peptideSequence, arrivalTime, xcorr, rtPeak, correctedRT, offset, originalRTCalcPredictedValue, isPredicted);
			return str;
		}
	}
}
