using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MealTimeMS.Properties;

namespace MealTimeMS.Util
{
	public static class InputFileOrganizer
	{
		//contains all the files, pretty self-explanatory

		//Directories
		public static String WorkingDirectory = "C:\\Coding\\2019LavalleeLab\\GitProjectRealTimeMS\\";
		public static String DataRoot = WorkingDirectory+"TestData\\";
		public static String PreComputedFilesRoot = DataRoot+"PreComputedFiles\\";
		public static String OutputRoot = WorkingDirectory + "Output\\";
		public static String OutputFolderOfTheRun;
		public static String preExperimentFilesFolder;
		public static String AssemblyDirectory = Path.GetDirectoryName( System.Reflection.Assembly.GetEntryAssembly().Location);

		//simulation files
		public static String MS2SimulationTestFile;
		public static String MZMLSimulationTestFile;

		//Files that are absolutely required
		public static String FASTA_FILE ; //the fasta used for external program generation.
		public static String CometParamsFile;
		public static String AccordNet_LogisticRegressionClassifier_WeightAndInterceptSavedFile;
		public static String dbFasta; //the fasta used to construct the in-program database graph. in most circumstances will be set to the same as FASTA_FILE
									  //quality check files
		public static  String CometQualityCheckFile = "";

		//Tools
		public const String RTCalc = "Tools\\RTCalc.exe";
		public const String RTCalcCoeff = "Tools\\RTCalc.coeff";
		//public const String RTCalc = "Tools\\RTCalc_2019.exe";
		//public const String RTCalcCoeff = "Tools\\rtcalc_120minTraining.coeff";
		public const String ChainSaw = "Tools\\chainsaw.exe";
		public const String CometExe = "Tools\\comet.2019011.win32.exe";
		private const String TPPBinFolder = "C:\\TPP\\bin\\";
		public const String XInteract = TPPBinFolder + "xinteract.exe";
		public const String ProteinProphet = TPPBinFolder + "ProteinProphet.exe";

		//pre computed files
		public static String RTCalcResult;
		public static String ChainSawResult;
		public static String OriginalCometOutput;
		public static String ProtXML;
		public static String DecoyFasta;
		public static String IDXDataBase;

		//ML files
		public static String MS2_ClassifierTraining;
		public static String MZML_ClassifierTraining;

		public static String IdentificationFeatureTrainingSet = DataRoot + "2019-07-11_Training_MS_QC_240min_IdentificationFeaturesFile.csv"; //contains decoys
																																			  //public static String IdentificationFeatureTrainingSet = DataRoot + "2019-06-28_Training_MS_QC_240min_IdentificationFeaturesFile.csv";
																																			  //public static String IdentificationFeatureTestingSet = DataRoot + "2019-07-11_Testing_MS_QC_120min_IdentificationFeaturesFile.csv"; //contains decoys
		public static String IdentificationFeatureTestingSet = DataRoot + "2019-06-28_Testing_MS_QC_120min_IdentificationFeaturesFile.csv";
		//suffixes
		public static String PepXMLSuffix = ".pep.xml";

		//test files
		public static String SVMSavedFile = "";

		public static void SetWorkDir(String workDir)
		{
			WorkingDirectory = workDir;
			DataRoot = WorkingDirectory + "TestData\\";
			OutputRoot = WorkingDirectory + "Output\\";
			PreComputedFilesRoot = DataRoot + "PreComputedFiles\\";
			if (!Directory.Exists(OutputRoot))
			{
				Directory.CreateDirectory(OutputRoot);
			}
			//if (!Directory.Exists(DataRoot))
			//{
			//	Directory.CreateDirectory(DataRoot);
			//}
			//if (!Directory.Exists(PreComputedFilesRoot))
			//{
			//	Directory.CreateDirectory(PreComputedFilesRoot);
			//}
			CometQualityCheckFile = Path.Combine(AssemblyDirectory, "EmbeddedDataFiles", "CometQualityCheck.txt");
		}
		//public static void SetWorkDir(String workDir)
		//{
		//	WorkingDirectory = workDir;
		//	DataRoot = WorkingDirectory + "TestData\\";
		//	OutputRoot = WorkingDirectory + "Output\\";
		//	PreComputedFilesRoot = DataRoot + "PreComputedFiles\\";
		
		//	if (!Directory.Exists(OutputRoot))
		//	{
		//		Directory.CreateDirectory(OutputRoot);
		//	}
		//	if (!Directory.Exists(DataRoot))
		//	{
		//		Directory.CreateDirectory(DataRoot);
		//	}
		//	if (!Directory.Exists(PreComputedFilesRoot))
		//	{
		//		Directory.CreateDirectory(PreComputedFilesRoot);
		//	}
		

		//	//Crucial files for simulation and experiment
		//	CometParamsFile = DataRoot + "2019.comet.params";

		//	CometQualityCheckFile = Path.Combine(AssemblyDirectory, "EmbeddedDataFiles", "CometQualityCheck.txt");
		//	AccordNet_LogisticRegressionClassifier_WeightAndInterceptSavedFile = DataRoot + "AccordWeight_DCN240Testing_prThr0.561.txt";
		//	//AccordNet_LogisticRegressionClassifier_WeightAndInterceptSavedFile = DataRoot + "AccordWeight_StDevCorrectedTraining.txt";
		//	//AccordNet_LogisticRegressionClassifier_WeightAndInterceptSavedFile = DataRoot + "AccordWeight_NoStDev.txt";
		//	SVMSavedFile = DataRoot + "SVMParams_TrainedOnDCN240BadPSM.txt";
			
		//	FASTA_FILE = DataRoot + "uniprot_SwissProt_Human_1_11_2017.fasta";
		//	dbFasta = FASTA_FILE;
		//	//FASTA_FILE = DataRoot + "PreComputedFiles\\uniprot_SwissProt_Human_1_11_2017_decoyConcacenated.fasta";

		//	//data files for simulation
		//	MS2SimulationTestFile = DataRoot + "MS_QC_120min.ms2";
			
		//	MZMLSimulationTestFile = DataRoot + "MS_QC_120min.mzML";
			

		//	//optional pre-processed files, can be generated by the program, or inputted here just to speed up test simulation speed
		//	DecoyFasta = PreComputedFilesRoot + "uniprot_SwissProt_Human_1_11_2017_decoyConcacenated.fasta";
		//	IDXDataBase = PreComputedFilesRoot + "uniprot_SwissProt_Human_1_11_2017_decoyConcacenated.fasta.idx";
		//	ChainSawResult = PreComputedFilesRoot + "uniprot_SwissProt_Human_1_11_2017.fasta_digestedPeptides.tsv";
		//	RTCalcResult = PreComputedFilesRoot + "tempOutputPeptideList_rtOutput.txt";
		//	//RTCalcResult = PreComputedFilesRoot + "tempOutputPeptideList_rtOutput_120minTrained.txt";
		//	ProtXML = PreComputedFilesRoot + "MS_QC_120min_interact.prot.xml";
		//	OriginalCometOutput = PreComputedFilesRoot + "MS_QC_120min.pep.xml";
		//}


	}


}
