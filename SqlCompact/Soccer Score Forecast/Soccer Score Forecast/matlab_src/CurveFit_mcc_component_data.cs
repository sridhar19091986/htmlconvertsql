//
// MATLAB Compiler: 4.7 (R2007b)
// Date: Mon Mar 07 10:38:31 2011
// Arguments: "-B" "macro_default" "-W" "dotnet:CurveFit,CurveFitclass,0.0,private" "-d"
// "D:\My Documents\MATLAB\CurveFit\src" "-T" "link:lib" "-v" "class{CurveFitclass:D:\My
// Documents\MATLAB\myfinemeshgrid.m,D:\My Documents\MATLAB\mygriddata.m,D:\My
// Documents\MATLAB\myinterp1.m,D:\My Documents\MATLAB\myinterp2.m,D:\My
// Documents\MATLAB\mymeshgrid.m,D:\My Documents\MATLAB\myplus.m,D:\My
// Documents\MATLAB\mypolyfit.m,D:\My Documents\MATLAB\mypolyval.m}" 
//


using System;

namespace CurveFit
{
  /// <summary>
  /// The MCR Component State
  /// </summary>
  public class MCRComponentState
    {
      /// <summary>
      /// The .NET Builder component name
      /// </summary>
      public static string MCC_CurveFit_name_data= "CurveFit";

      /// <summary>
      /// The component root data
      /// </summary>
      public static string MCC_CurveFit_root_data= "";

      /// <summary>
      /// The public encryption key for the .NET Builder component
      /// </summary>
      public static byte[] MCC_CurveFit_public_data=
                              {(byte)'3', (byte)'0', (byte)'8', (byte)'1',
                               (byte)'9', (byte)'D', (byte)'3', (byte)'0',
                               (byte)'0', (byte)'D', (byte)'0', (byte)'6',
                               (byte)'0', (byte)'9', (byte)'2', (byte)'A',
                               (byte)'8', (byte)'6', (byte)'4', (byte)'8',
                               (byte)'8', (byte)'6', (byte)'F', (byte)'7',
                               (byte)'0', (byte)'D', (byte)'0', (byte)'1',
                               (byte)'0', (byte)'1', (byte)'0', (byte)'1',
                               (byte)'0', (byte)'5', (byte)'0', (byte)'0',
                               (byte)'0', (byte)'3', (byte)'8', (byte)'1',
                               (byte)'8', (byte)'B', (byte)'0', (byte)'0',
                               (byte)'3', (byte)'0', (byte)'8', (byte)'1',
                               (byte)'8', (byte)'7', (byte)'0', (byte)'2',
                               (byte)'8', (byte)'1', (byte)'8', (byte)'1',
                               (byte)'0', (byte)'0', (byte)'C', (byte)'4',
                               (byte)'9', (byte)'C', (byte)'A', (byte)'C',
                               (byte)'3', (byte)'4', (byte)'E', (byte)'D',
                               (byte)'1', (byte)'3', (byte)'A', (byte)'5',
                               (byte)'2', (byte)'0', (byte)'6', (byte)'5',
                               (byte)'8', (byte)'F', (byte)'6', (byte)'F',
                               (byte)'8', (byte)'E', (byte)'0', (byte)'1',
                               (byte)'3', (byte)'8', (byte)'C', (byte)'4',
                               (byte)'3', (byte)'1', (byte)'5', (byte)'B',
                               (byte)'4', (byte)'3', (byte)'1', (byte)'5',
                               (byte)'2', (byte)'7', (byte)'7', (byte)'E',
                               (byte)'D', (byte)'3', (byte)'F', (byte)'7',
                               (byte)'D', (byte)'A', (byte)'E', (byte)'5',
                               (byte)'3', (byte)'0', (byte)'9', (byte)'9',
                               (byte)'D', (byte)'B', (byte)'0', (byte)'8',
                               (byte)'E', (byte)'E', (byte)'5', (byte)'8',
                               (byte)'9', (byte)'F', (byte)'8', (byte)'0',
                               (byte)'4', (byte)'D', (byte)'4', (byte)'B',
                               (byte)'9', (byte)'8', (byte)'1', (byte)'3',
                               (byte)'2', (byte)'6', (byte)'A', (byte)'5',
                               (byte)'2', (byte)'C', (byte)'C', (byte)'E',
                               (byte)'4', (byte)'3', (byte)'8', (byte)'2',
                               (byte)'E', (byte)'9', (byte)'F', (byte)'2',
                               (byte)'B', (byte)'4', (byte)'D', (byte)'0',
                               (byte)'8', (byte)'5', (byte)'E', (byte)'B',
                               (byte)'9', (byte)'5', (byte)'0', (byte)'C',
                               (byte)'7', (byte)'A', (byte)'B', (byte)'1',
                               (byte)'2', (byte)'E', (byte)'D', (byte)'E',
                               (byte)'2', (byte)'D', (byte)'4', (byte)'1',
                               (byte)'2', (byte)'9', (byte)'7', (byte)'8',
                               (byte)'2', (byte)'0', (byte)'E', (byte)'6',
                               (byte)'3', (byte)'7', (byte)'7', (byte)'A',
                               (byte)'5', (byte)'F', (byte)'E', (byte)'B',
                               (byte)'5', (byte)'6', (byte)'8', (byte)'9',
                               (byte)'D', (byte)'4', (byte)'E', (byte)'6',
                               (byte)'0', (byte)'3', (byte)'2', (byte)'F',
                               (byte)'6', (byte)'0', (byte)'C', (byte)'4',
                               (byte)'3', (byte)'0', (byte)'7', (byte)'4',
                               (byte)'A', (byte)'0', (byte)'4', (byte)'C',
                               (byte)'2', (byte)'6', (byte)'A', (byte)'B',
                               (byte)'7', (byte)'2', (byte)'F', (byte)'5',
                               (byte)'4', (byte)'B', (byte)'5', (byte)'1',
                               (byte)'B', (byte)'B', (byte)'4', (byte)'6',
                               (byte)'0', (byte)'5', (byte)'7', (byte)'8',
                               (byte)'7', (byte)'8', (byte)'5', (byte)'B',
                               (byte)'1', (byte)'9', (byte)'9', (byte)'0',
                               (byte)'1', (byte)'4', (byte)'3', (byte)'1',
                               (byte)'4', (byte)'A', (byte)'6', (byte)'5',
                               (byte)'F', (byte)'0', (byte)'9', (byte)'0',
                               (byte)'B', (byte)'6', (byte)'1', (byte)'F',
                               (byte)'C', (byte)'2', (byte)'0', (byte)'1',
                               (byte)'6', (byte)'9', (byte)'4', (byte)'5',
                               (byte)'3', (byte)'B', (byte)'5', (byte)'8',
                               (byte)'F', (byte)'C', (byte)'8', (byte)'B',
                               (byte)'A', (byte)'4', (byte)'3', (byte)'E',
                               (byte)'6', (byte)'7', (byte)'7', (byte)'6',
                               (byte)'E', (byte)'B', (byte)'7', (byte)'E',
                               (byte)'C', (byte)'D', (byte)'3', (byte)'1',
                               (byte)'7', (byte)'8', (byte)'B', (byte)'5',
                               (byte)'6', (byte)'A', (byte)'B', (byte)'0',
                               (byte)'F', (byte)'A', (byte)'0', (byte)'6',
                               (byte)'D', (byte)'D', (byte)'6', (byte)'4',
                               (byte)'9', (byte)'6', (byte)'7', (byte)'C',
                               (byte)'B', (byte)'1', (byte)'4', (byte)'9',
                               (byte)'E', (byte)'5', (byte)'0', (byte)'2',
                               (byte)'0', (byte)'1', (byte)'1', (byte)'1'};

      /// <summary>
      /// The session encryption key for the .NET Builder component
      /// </summary>
      public static byte[] MCC_CurveFit_session_data=
                              {(byte)'A', (byte)'3', (byte)'F', (byte)'F',
                               (byte)'1', (byte)'0', (byte)'D', (byte)'E',
                               (byte)'6', (byte)'2', (byte)'6', (byte)'6',
                               (byte)'9', (byte)'8', (byte)'9', (byte)'4',
                               (byte)'5', (byte)'4', (byte)'5', (byte)'0',
                               (byte)'8', (byte)'0', (byte)'9', (byte)'2',
                               (byte)'D', (byte)'C', (byte)'F', (byte)'4',
                               (byte)'1', (byte)'C', (byte)'E', (byte)'5',
                               (byte)'8', (byte)'A', (byte)'0', (byte)'4',
                               (byte)'D', (byte)'5', (byte)'B', (byte)'4',
                               (byte)'7', (byte)'4', (byte)'1', (byte)'2',
                               (byte)'8', (byte)'D', (byte)'9', (byte)'1',
                               (byte)'2', (byte)'9', (byte)'7', (byte)'2',
                               (byte)'B', (byte)'F', (byte)'2', (byte)'9',
                               (byte)'8', (byte)'3', (byte)'D', (byte)'E',
                               (byte)'D', (byte)'4', (byte)'D', (byte)'8',
                               (byte)'3', (byte)'0', (byte)'5', (byte)'E',
                               (byte)'3', (byte)'4', (byte)'9', (byte)'A',
                               (byte)'4', (byte)'D', (byte)'5', (byte)'8',
                               (byte)'A', (byte)'A', (byte)'0', (byte)'2',
                               (byte)'B', (byte)'4', (byte)'4', (byte)'F',
                               (byte)'8', (byte)'1', (byte)'3', (byte)'8',
                               (byte)'C', (byte)'6', (byte)'0', (byte)'B',
                               (byte)'B', (byte)'2', (byte)'6', (byte)'E',
                               (byte)'F', (byte)'7', (byte)'9', (byte)'E',
                               (byte)'F', (byte)'2', (byte)'2', (byte)'1',
                               (byte)'D', (byte)'8', (byte)'C', (byte)'4',
                               (byte)'5', (byte)'9', (byte)'0', (byte)'3',
                               (byte)'F', (byte)'6', (byte)'1', (byte)'0',
                               (byte)'C', (byte)'8', (byte)'B', (byte)'2',
                               (byte)'1', (byte)'E', (byte)'1', (byte)'C',
                               (byte)'B', (byte)'4', (byte)'1', (byte)'0',
                               (byte)'6', (byte)'0', (byte)'7', (byte)'E',
                               (byte)'E', (byte)'8', (byte)'4', (byte)'F',
                               (byte)'B', (byte)'E', (byte)'4', (byte)'E',
                               (byte)'2', (byte)'D', (byte)'F', (byte)'D',
                               (byte)'8', (byte)'F', (byte)'9', (byte)'9',
                               (byte)'3', (byte)'6', (byte)'5', (byte)'E',
                               (byte)'5', (byte)'6', (byte)'1', (byte)'2',
                               (byte)'0', (byte)'3', (byte)'7', (byte)'A',
                               (byte)'6', (byte)'D', (byte)'4', (byte)'5',
                               (byte)'0', (byte)'4', (byte)'6', (byte)'9',
                               (byte)'E', (byte)'F', (byte)'4', (byte)'B',
                               (byte)'F', (byte)'9', (byte)'1', (byte)'B',
                               (byte)'C', (byte)'0', (byte)'5', (byte)'B',
                               (byte)'0', (byte)'E', (byte)'1', (byte)'9',
                               (byte)'0', (byte)'3', (byte)'6', (byte)'B',
                               (byte)'9', (byte)'6', (byte)'C', (byte)'3',
                               (byte)'1', (byte)'A', (byte)'3', (byte)'B',
                               (byte)'5', (byte)'C', (byte)'9', (byte)'9',
                               (byte)'1', (byte)'3', (byte)'F', (byte)'4',
                               (byte)'E', (byte)'B', (byte)'B', (byte)'6',
                               (byte)'C', (byte)'0', (byte)'5', (byte)'A',
                               (byte)'1', (byte)'8', (byte)'1', (byte)'D',
                               (byte)'5', (byte)'3', (byte)'C', (byte)'0',
                               (byte)'C', (byte)'2', (byte)'F', (byte)'A',
                               (byte)'E', (byte)'C', (byte)'3', (byte)'0',
                               (byte)'C', (byte)'C', (byte)'6', (byte)'9',
                               (byte)'D', (byte)'9', (byte)'9', (byte)'8',
                               (byte)'7', (byte)'A', (byte)'8', (byte)'A',
                               (byte)'5', (byte)'7', (byte)'9', (byte)'A',
                               (byte)'7', (byte)'2', (byte)'8', (byte)'B',
                               (byte)'D', (byte)'0', (byte)'7', (byte)'B',
                               (byte)'C', (byte)'9', (byte)'C', (byte)'6'};

      /// <summary>
      /// The MATLAB path for the .NET Builder component
      /// </summary>
      public static string[] MCC_CurveFit_matlabpath_data= {"CurveFit/",
                                                            "toolbox/compiler/deploy/",
                                                            "$TOOLBOXMATLABDIR/general/",
                                                            "$TOOLBOXMATLABDIR/ops/",
                                                            "$TOOLBOXMATLABDIR/lang/",
                                                            "$TOOLBOXMATLABDIR/elmat/",
                                                            "$TOOLBOXMATLABDIR/elfun/",
                                                            "$TOOLBOXMATLABDIR/specfun/",
                                                            "$TOOLBOXMATLABDIR/matfun/",
                                                            "$TOOLBOXMATLABDIR/datafun/",
                                                            "$TOOLBOXMATLABDIR/polyfun/",
                                                            "$TOOLBOXMATLABDIR/funfun/",
                                                            "$TOOLBOXMATLABDIR/sparfun/",
                                                            "$TOOLBOXMATLABDIR/scribe/",
                                                            "$TOOLBOXMATLABDIR/graph2d/",
                                                            "$TOOLBOXMATLABDIR/graph3d/",
                                                            "$TOOLBOXMATLABDIR/specgraph/",
                                                            "$TOOLBOXMATLABDIR/graphics/",
                                                            "$TOOLBOXMATLABDIR/uitools/",
                                                            "$TOOLBOXMATLABDIR/strfun/",
                                                            "$TOOLBOXMATLABDIR/imagesci/",
                                                            "$TOOLBOXMATLABDIR/iofun/",
                                                            "$TOOLBOXMATLABDIR/audiovideo/",
                                                            "$TOOLBOXMATLABDIR/timefun/",
                                                            "$TOOLBOXMATLABDIR/datatypes/",
                                                            "$TOOLBOXMATLABDIR/verctrl/",
                                                            "$TOOLBOXMATLABDIR/codetools/",
                                                            "$TOOLBOXMATLABDIR/helptools/",
                                                            "$TOOLBOXMATLABDIR/winfun/",
                                                            "$TOOLBOXMATLABDIR/demos/",
                                                            "$TOOLBOXMATLABDIR/timeseries/",
                                                            "$TOOLBOXMATLABDIR/hds/",
                                                            "$TOOLBOXMATLABDIR/guide/",
                                                            "$TOOLBOXMATLABDIR/plottools/",
                                                            "toolbox/local/"};
      /// <summary>
      /// The MATLAB path count
      /// </summary>
      public static int MCC_CurveFit_matlabpath_data_count= 35;

      /// <summary>
      /// The class path for the .NET Builder component
      /// </summary>
      public static string[] MCC_CurveFit_classpath_data= {"\0"};

      /// <summary>
      /// The class path count
      /// </summary>
      public static int MCC_CurveFit_classpath_data_count= 0;

      /// <summary>
      /// The lib path for the .NET Builder component
      /// </summary>
      public static string[] MCC_CurveFit_libpath_data= {"\0"};

      /// <summary>
      /// The lib path count
      /// </summary>
      public static int MCC_CurveFit_libpath_data_count= 0;

      /// <summary>
      /// The MCR application options
      /// </summary>
      public static string[] MCC_CurveFit_mcr_application_options= {"\0"};

      /// <summary>
      /// The MCR application options count
      /// </summary>
      public static int MCC_CurveFit_mcr_application_option_count= 0;

      /// <summary>
      /// The MCR runtime options
      /// </summary>
      public static string[] MCC_CurveFit_mcr_runtime_options= {"\0"};

      /// <summary>
      /// The MCR runtime options count
      /// </summary>
      public static int MCC_CurveFit_mcr_runtime_option_count= 0;

      /// <summary>
      /// The component preferences directory
      /// </summary>
      public static string MCC_CurveFit_mcr_pref_dir= "CurveFit_A60376416E5B4CFD68018CE043C55C34";

      /// <summary>
      /// Runtime warning states
      /// </summary>
      public static string[] MCC_CurveFit_set_warning_state= {"off:MATLAB:dispatcher:nameConflict"};
      /// <summary>
      /// Runtime warning state count
      /// </summary>
      public static int MCC_CurveFit_set_warning_state_count= 0;
    }
}
