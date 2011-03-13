/*
* MATLAB Compiler: 4.7 (R2007b)
* Date: Mon Mar 07 10:38:31 2011
* Arguments: "-B" "macro_default" "-W" "dotnet:CurveFit,CurveFitclass,0.0,private" "-d"
* "D:\My Documents\MATLAB\CurveFit\src" "-T" "link:lib" "-v" "class{CurveFitclass:D:\My
* Documents\MATLAB\myfinemeshgrid.m,D:\My Documents\MATLAB\mygriddata.m,D:\My
* Documents\MATLAB\myinterp1.m,D:\My Documents\MATLAB\myinterp2.m,D:\My
* Documents\MATLAB\mymeshgrid.m,D:\My Documents\MATLAB\myplus.m,D:\My
* Documents\MATLAB\mypolyfit.m,D:\My Documents\MATLAB\mypolyval.m}" 
*/

using System;
using System.Reflection;

using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;


[assembly: System.Reflection.AssemblyVersion("0.0.*")]
#if SHARED
[assembly: System.Reflection.AssemblyKeyFile(@"")]
#endif

namespace CurveFit
{
  /// <summary>
  /// The CurveFitclass class provides a CLS compliant interface to the M-functions
  /// contained in the files:
  /// <newpara></newpara>
  /// D:\My Documents\MATLAB\myfinemeshgrid.m
  /// <newpara></newpara>
  /// D:\My Documents\MATLAB\mygriddata.m
  /// <newpara></newpara>
  /// D:\My Documents\MATLAB\myinterp1.m
  /// <newpara></newpara>
  /// D:\My Documents\MATLAB\myinterp2.m
  /// <newpara></newpara>
  /// D:\My Documents\MATLAB\mymeshgrid.m
  /// <newpara></newpara>
  /// D:\My Documents\MATLAB\myplus.m
  /// <newpara></newpara>
  /// D:\My Documents\MATLAB\mypolyfit.m
  /// <newpara></newpara>
  /// D:\My Documents\MATLAB\mypolyval.m
  /// <newpara></newpara>
  /// deployprint.m
  /// <newpara></newpara>
  /// printdlg.m
  /// </summary>
  /// <remarks>
  /// @Version 0.0
  /// </remarks>
  public class CurveFitclass : IDisposable
    {
      #region Constructors

      /// <summary internal= "true">
      /// The static constructor instantiates and initializes the MATLAB Component
      /// Runtime instance.
      /// </summary>
      static CurveFitclass()
        {
          if (MWArray.MCRAppInitialized)
            {
              Assembly assembly= Assembly.GetExecutingAssembly();

              string ctfFilePath= assembly.Location;

              int lastDelimeter= ctfFilePath.LastIndexOf(@"\");

              ctfFilePath= ctfFilePath.Remove(lastDelimeter, (ctfFilePath.Length - lastDelimeter));

              mcr= new MWMCR(MCRComponentState.MCC_CurveFit_name_data,
                             MCRComponentState.MCC_CurveFit_root_data,
                             MCRComponentState.MCC_CurveFit_public_data,
                             MCRComponentState.MCC_CurveFit_session_data,
                             MCRComponentState.MCC_CurveFit_matlabpath_data,
                             MCRComponentState.MCC_CurveFit_classpath_data,
                             MCRComponentState.MCC_CurveFit_libpath_data,
                             MCRComponentState.MCC_CurveFit_mcr_application_options,
                             MCRComponentState.MCC_CurveFit_mcr_runtime_options,
                             MCRComponentState.MCC_CurveFit_mcr_pref_dir,
                             MCRComponentState.MCC_CurveFit_set_warning_state,
                             ctfFilePath, true);
            }
          else
            {
              throw new ApplicationException("MWArray assembly could not be initialized");
            }
        }


      /// <summary>
      /// Constructs a new instance of the CurveFitclass class.
      /// </summary>
      public CurveFitclass()
        {
        }


      #endregion Constructors

      #region Finalize

      /// <summary internal= "true">
      /// Class destructor called by the CLR garbage collector.
      /// </summary>
      ~CurveFitclass()
        {
          Dispose(false);
        }


      /// <summary>
      /// Frees the native resources associated with this object
      /// </summary>
      public void Dispose()
        {
          Dispose(true);

          GC.SuppressFinalize(this);
        }


      /// <summary internal= "true">
      /// Internal dispose function
      /// </summary>
      protected virtual void Dispose(bool disposing)
        {
          if (!disposed)
            {
              disposed= true;

              if (disposing)
                {
                  // Free managed resources;
                }

              // Free native resources
            }
        }


      #endregion Finalize

      #region Methods

      /// <summary>
      /// Provides a single output, 0-input interface to the myfinemeshgrid M-function.
      /// </summary>
      /// <remarks>
      /// M-Documentation:
      /// Using two colons to create a vector with increments between
      /// first and end elements.
      /// </remarks>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray myfinemeshgrid()
        {
          return mcr.EvaluateFunction("myfinemeshgrid");
        }


      /// <summary>
      /// Provides a single output, 1-input interface to the myfinemeshgrid M-function.
      /// </summary>
      /// <remarks>
      /// M-Documentation:
      /// Using two colons to create a vector with increments between
      /// first and end elements.
      /// </remarks>
      /// <param name="vectorstepx">Input argument #1</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray myfinemeshgrid(MWArray vectorstepx)
        {
          return mcr.EvaluateFunction("myfinemeshgrid", vectorstepx);
        }


      /// <summary>
      /// Provides a single output, 2-input interface to the myfinemeshgrid M-function.
      /// </summary>
      /// <remarks>
      /// M-Documentation:
      /// Using two colons to create a vector with increments between
      /// first and end elements.
      /// </remarks>
      /// <param name="vectorstepx">Input argument #1</param>
      /// <param name="vectorstepy">Input argument #2</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray myfinemeshgrid(MWArray vectorstepx, MWArray vectorstepy)
        {
          return mcr.EvaluateFunction("myfinemeshgrid",
                                      vectorstepx, vectorstepy);
        }


      /// <summary>
      /// Provides the standard 0-input interface to the myfinemeshgrid M-function.
      /// </summary>
      /// <remarks>
      /// M-Documentation:
      /// Using two colons to create a vector with increments between
      /// first and end elements.
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] myfinemeshgrid(int numArgsOut)
        {
          return mcr.EvaluateFunction(numArgsOut, "myfinemeshgrid");
        }


      /// <summary>
      /// Provides the standard 1-input interface to the myfinemeshgrid M-function.
      /// </summary>
      /// <remarks>
      /// M-Documentation:
      /// Using two colons to create a vector with increments between
      /// first and end elements.
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="vectorstepx">Input argument #1</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] myfinemeshgrid(int numArgsOut, MWArray vectorstepx)
        {
          return mcr.EvaluateFunction(numArgsOut, "myfinemeshgrid",
                                      vectorstepx);
        }


      /// <summary>
      /// Provides the standard 2-input interface to the myfinemeshgrid M-function.
      /// </summary>
      /// <remarks>
      /// M-Documentation:
      /// Using two colons to create a vector with increments between
      /// first and end elements.
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="vectorstepx">Input argument #1</param>
      /// <param name="vectorstepy">Input argument #2</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] myfinemeshgrid(int numArgsOut, MWArray vectorstepx,
                                      MWArray vectorstepy)
        {
          return mcr.EvaluateFunction(numArgsOut, "myfinemeshgrid",
                                      vectorstepx, vectorstepy);
        }


      /// <summary>
      /// Provides an interface for the myfinemeshgrid function in which the input and
      /// output
      /// arguments are specified as an array of MWArrays.
      /// </summary>
      /// <remarks>
      /// This method will allocate and return by reference the output argument
      /// array.<newpara></newpara>
      /// M-Documentation:
      /// Using two colons to create a vector with increments between
      /// first and end elements.
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return</param>
      /// <param name= "argsOut">Array of MWArray output arguments</param>
      /// <param name= "argsIn">Array of MWArray input arguments</param>
      ///
      public void myfinemeshgrid(int numArgsOut, ref MWArray[] argsOut,
                           MWArray[] argsIn)
        {
          mcr.EvaluateFunction("myfinemeshgrid", numArgsOut, ref argsOut, argsIn);
        }


      /// <summary>
      /// Provides a single output, 0-input interface to the mygriddata M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray mygriddata()
        {
          return mcr.EvaluateFunction("mygriddata");
        }


      /// <summary>
      /// Provides a single output, 1-input interface to the mygriddata M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="x">Input argument #1</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray mygriddata(MWArray x)
        {
          return mcr.EvaluateFunction("mygriddata", x);
        }


      /// <summary>
      /// Provides a single output, 2-input interface to the mygriddata M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="x">Input argument #1</param>
      /// <param name="y">Input argument #2</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray mygriddata(MWArray x, MWArray y)
        {
          return mcr.EvaluateFunction("mygriddata", x, y);
        }


      /// <summary>
      /// Provides a single output, 3-input interface to the mygriddata M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="x">Input argument #1</param>
      /// <param name="y">Input argument #2</param>
      /// <param name="z">Input argument #3</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray mygriddata(MWArray x, MWArray y, MWArray z)
        {
          return mcr.EvaluateFunction("mygriddata", x, y, z);
        }


      /// <summary>
      /// Provides a single output, 4-input interface to the mygriddata M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="x">Input argument #1</param>
      /// <param name="y">Input argument #2</param>
      /// <param name="z">Input argument #3</param>
      /// <param name="XI">Input argument #4</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray mygriddata(MWArray x, MWArray y, MWArray z, MWArray XI)
        {
          return mcr.EvaluateFunction("mygriddata", x, y, z, XI);
        }


      /// <summary>
      /// Provides a single output, 5-input interface to the mygriddata M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="x">Input argument #1</param>
      /// <param name="y">Input argument #2</param>
      /// <param name="z">Input argument #3</param>
      /// <param name="XI">Input argument #4</param>
      /// <param name="YI">Input argument #5</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray mygriddata(MWArray x, MWArray y, MWArray z,
                                MWArray XI, MWArray YI)
        {
          return mcr.EvaluateFunction("mygriddata", x, y, z, XI, YI);
        }


      /// <summary>
      /// Provides the standard 0-input interface to the mygriddata M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] mygriddata(int numArgsOut)
        {
          return mcr.EvaluateFunction(numArgsOut, "mygriddata");
        }


      /// <summary>
      /// Provides the standard 1-input interface to the mygriddata M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="x">Input argument #1</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] mygriddata(int numArgsOut, MWArray x)
        {
          return mcr.EvaluateFunction(numArgsOut, "mygriddata", x);
        }


      /// <summary>
      /// Provides the standard 2-input interface to the mygriddata M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="x">Input argument #1</param>
      /// <param name="y">Input argument #2</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] mygriddata(int numArgsOut, MWArray x, MWArray y)
        {
          return mcr.EvaluateFunction(numArgsOut, "mygriddata", x, y);
        }


      /// <summary>
      /// Provides the standard 3-input interface to the mygriddata M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="x">Input argument #1</param>
      /// <param name="y">Input argument #2</param>
      /// <param name="z">Input argument #3</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] mygriddata(int numArgsOut, MWArray x,
                                  MWArray y, MWArray z)
        {
          return mcr.EvaluateFunction(numArgsOut, "mygriddata", x, y, z);
        }


      /// <summary>
      /// Provides the standard 4-input interface to the mygriddata M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="x">Input argument #1</param>
      /// <param name="y">Input argument #2</param>
      /// <param name="z">Input argument #3</param>
      /// <param name="XI">Input argument #4</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] mygriddata(int numArgsOut, MWArray x,
                                  MWArray y, MWArray z, MWArray XI)
        {
          return mcr.EvaluateFunction(numArgsOut, "mygriddata", x, y, z, XI);
        }


      /// <summary>
      /// Provides the standard 5-input interface to the mygriddata M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="x">Input argument #1</param>
      /// <param name="y">Input argument #2</param>
      /// <param name="z">Input argument #3</param>
      /// <param name="XI">Input argument #4</param>
      /// <param name="YI">Input argument #5</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] mygriddata(int numArgsOut, MWArray x, MWArray y,
                                  MWArray z, MWArray XI, MWArray YI)
        {
          return mcr.EvaluateFunction(numArgsOut, "mygriddata",
                                      x, y, z, XI, YI);
        }


      /// <summary>
      /// Provides an interface for the mygriddata function in which the input and output
      /// arguments are specified as an array of MWArrays.
      /// </summary>
      /// <remarks>
      /// This method will allocate and return by reference the output argument
      /// array.<newpara></newpara>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return</param>
      /// <param name= "argsOut">Array of MWArray output arguments</param>
      /// <param name= "argsIn">Array of MWArray input arguments</param>
      ///
      public void mygriddata(int numArgsOut, ref MWArray[] argsOut, MWArray[] argsIn)
        {
          mcr.EvaluateFunction("mygriddata", numArgsOut, ref argsOut, argsIn);
        }


      /// <summary>
      /// Provides a single output, 0-input interface to the myinterp1 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray myinterp1()
        {
          return mcr.EvaluateFunction("myinterp1");
        }


      /// <summary>
      /// Provides a single output, 1-input interface to the myinterp1 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="x">Input argument #1</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray myinterp1(MWArray x)
        {
          return mcr.EvaluateFunction("myinterp1", x);
        }


      /// <summary>
      /// Provides a single output, 2-input interface to the myinterp1 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="x">Input argument #1</param>
      /// <param name="y">Input argument #2</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray myinterp1(MWArray x, MWArray y)
        {
          return mcr.EvaluateFunction("myinterp1", x, y);
        }


      /// <summary>
      /// Provides a single output, 3-input interface to the myinterp1 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="x">Input argument #1</param>
      /// <param name="y">Input argument #2</param>
      /// <param name="xi">Input argument #3</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray myinterp1(MWArray x, MWArray y, MWArray xi)
        {
          return mcr.EvaluateFunction("myinterp1", x, y, xi);
        }


      /// <summary>
      /// Provides the standard 0-input interface to the myinterp1 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] myinterp1(int numArgsOut)
        {
          return mcr.EvaluateFunction(numArgsOut, "myinterp1");
        }


      /// <summary>
      /// Provides the standard 1-input interface to the myinterp1 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="x">Input argument #1</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] myinterp1(int numArgsOut, MWArray x)
        {
          return mcr.EvaluateFunction(numArgsOut, "myinterp1", x);
        }


      /// <summary>
      /// Provides the standard 2-input interface to the myinterp1 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="x">Input argument #1</param>
      /// <param name="y">Input argument #2</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] myinterp1(int numArgsOut, MWArray x, MWArray y)
        {
          return mcr.EvaluateFunction(numArgsOut, "myinterp1", x, y);
        }


      /// <summary>
      /// Provides the standard 3-input interface to the myinterp1 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="x">Input argument #1</param>
      /// <param name="y">Input argument #2</param>
      /// <param name="xi">Input argument #3</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] myinterp1(int numArgsOut, MWArray x,
                                 MWArray y, MWArray xi)
        {
          return mcr.EvaluateFunction(numArgsOut, "myinterp1", x, y, xi);
        }


      /// <summary>
      /// Provides an interface for the myinterp1 function in which the input and output
      /// arguments are specified as an array of MWArrays.
      /// </summary>
      /// <remarks>
      /// This method will allocate and return by reference the output argument
      /// array.<newpara></newpara>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return</param>
      /// <param name= "argsOut">Array of MWArray output arguments</param>
      /// <param name= "argsIn">Array of MWArray input arguments</param>
      ///
      public void myinterp1(int numArgsOut, ref MWArray[] argsOut, MWArray[] argsIn)
        {
          mcr.EvaluateFunction("myinterp1", numArgsOut, ref argsOut, argsIn);
        }


      /// <summary>
      /// Provides a single output, 0-input interface to the myinterp2 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray myinterp2()
        {
          return mcr.EvaluateFunction("myinterp2");
        }


      /// <summary>
      /// Provides a single output, 1-input interface to the myinterp2 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="X">Input argument #1</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray myinterp2(MWArray X)
        {
          return mcr.EvaluateFunction("myinterp2", X);
        }


      /// <summary>
      /// Provides a single output, 2-input interface to the myinterp2 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="X">Input argument #1</param>
      /// <param name="Y">Input argument #2</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray myinterp2(MWArray X, MWArray Y)
        {
          return mcr.EvaluateFunction("myinterp2", X, Y);
        }


      /// <summary>
      /// Provides a single output, 3-input interface to the myinterp2 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="X">Input argument #1</param>
      /// <param name="Y">Input argument #2</param>
      /// <param name="Z">Input argument #3</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray myinterp2(MWArray X, MWArray Y, MWArray Z)
        {
          return mcr.EvaluateFunction("myinterp2", X, Y, Z);
        }


      /// <summary>
      /// Provides a single output, 4-input interface to the myinterp2 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="X">Input argument #1</param>
      /// <param name="Y">Input argument #2</param>
      /// <param name="Z">Input argument #3</param>
      /// <param name="XI">Input argument #4</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray myinterp2(MWArray X, MWArray Y, MWArray Z, MWArray XI)
        {
          return mcr.EvaluateFunction("myinterp2", X, Y, Z, XI);
        }


      /// <summary>
      /// Provides a single output, 5-input interface to the myinterp2 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="X">Input argument #1</param>
      /// <param name="Y">Input argument #2</param>
      /// <param name="Z">Input argument #3</param>
      /// <param name="XI">Input argument #4</param>
      /// <param name="YI">Input argument #5</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray myinterp2(MWArray X, MWArray Y, MWArray Z,
                               MWArray XI, MWArray YI)
        {
          return mcr.EvaluateFunction("myinterp2", X, Y, Z, XI, YI);
        }


      /// <summary>
      /// Provides a single output, 6-input interface to the myinterp2 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="X">Input argument #1</param>
      /// <param name="Y">Input argument #2</param>
      /// <param name="Z">Input argument #3</param>
      /// <param name="XI">Input argument #4</param>
      /// <param name="YI">Input argument #5</param>
      /// <param name="method">Input argument #6</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray myinterp2(MWArray X, MWArray Y, MWArray Z,
                               MWArray XI, MWArray YI, MWArray method)
        {
          return mcr.EvaluateFunction("myinterp2", X, Y, Z, XI, YI, method);
        }


      /// <summary>
      /// Provides the standard 0-input interface to the myinterp2 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] myinterp2(int numArgsOut)
        {
          return mcr.EvaluateFunction(numArgsOut, "myinterp2");
        }


      /// <summary>
      /// Provides the standard 1-input interface to the myinterp2 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="X">Input argument #1</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] myinterp2(int numArgsOut, MWArray X)
        {
          return mcr.EvaluateFunction(numArgsOut, "myinterp2", X);
        }


      /// <summary>
      /// Provides the standard 2-input interface to the myinterp2 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="X">Input argument #1</param>
      /// <param name="Y">Input argument #2</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] myinterp2(int numArgsOut, MWArray X, MWArray Y)
        {
          return mcr.EvaluateFunction(numArgsOut, "myinterp2", X, Y);
        }


      /// <summary>
      /// Provides the standard 3-input interface to the myinterp2 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="X">Input argument #1</param>
      /// <param name="Y">Input argument #2</param>
      /// <param name="Z">Input argument #3</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] myinterp2(int numArgsOut, MWArray X,
                                 MWArray Y, MWArray Z)
        {
          return mcr.EvaluateFunction(numArgsOut, "myinterp2", X, Y, Z);
        }


      /// <summary>
      /// Provides the standard 4-input interface to the myinterp2 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="X">Input argument #1</param>
      /// <param name="Y">Input argument #2</param>
      /// <param name="Z">Input argument #3</param>
      /// <param name="XI">Input argument #4</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] myinterp2(int numArgsOut, MWArray X,
                                 MWArray Y, MWArray Z, MWArray XI)
        {
          return mcr.EvaluateFunction(numArgsOut, "myinterp2", X, Y, Z, XI);
        }


      /// <summary>
      /// Provides the standard 5-input interface to the myinterp2 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="X">Input argument #1</param>
      /// <param name="Y">Input argument #2</param>
      /// <param name="Z">Input argument #3</param>
      /// <param name="XI">Input argument #4</param>
      /// <param name="YI">Input argument #5</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] myinterp2(int numArgsOut, MWArray X, MWArray Y,
                                 MWArray Z, MWArray XI, MWArray YI)
        {
          return mcr.EvaluateFunction(numArgsOut, "myinterp2", X, Y, Z, XI, YI);
        }


      /// <summary>
      /// Provides the standard 6-input interface to the myinterp2 M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="X">Input argument #1</param>
      /// <param name="Y">Input argument #2</param>
      /// <param name="Z">Input argument #3</param>
      /// <param name="XI">Input argument #4</param>
      /// <param name="YI">Input argument #5</param>
      /// <param name="method">Input argument #6</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] myinterp2(int numArgsOut, MWArray X,
                                 MWArray Y, MWArray Z, MWArray XI,
                                 MWArray YI, MWArray method)
        {
          return mcr.EvaluateFunction(numArgsOut, "myinterp2",
                                      X, Y, Z, XI, YI, method);
        }


      /// <summary>
      /// Provides an interface for the myinterp2 function in which the input and output
      /// arguments are specified as an array of MWArrays.
      /// </summary>
      /// <remarks>
      /// This method will allocate and return by reference the output argument
      /// array.<newpara></newpara>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return</param>
      /// <param name= "argsOut">Array of MWArray output arguments</param>
      /// <param name= "argsIn">Array of MWArray input arguments</param>
      ///
      public void myinterp2(int numArgsOut, ref MWArray[] argsOut, MWArray[] argsIn)
        {
          mcr.EvaluateFunction("myinterp2", numArgsOut, ref argsOut, argsIn);
        }


      /// <summary>
      /// Provides a single output, 0-input interface to the mymeshgrid M-function.
      /// </summary>
      /// <remarks>
      /// M-Documentation:
      /// Using two colons to create a vector with increments between
      /// first and end elements.
      /// </remarks>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray mymeshgrid()
        {
          return mcr.EvaluateFunction("mymeshgrid");
        }


      /// <summary>
      /// Provides a single output, 1-input interface to the mymeshgrid M-function.
      /// </summary>
      /// <remarks>
      /// M-Documentation:
      /// Using two colons to create a vector with increments between
      /// first and end elements.
      /// </remarks>
      /// <param name="vectorstepx">Input argument #1</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray mymeshgrid(MWArray vectorstepx)
        {
          return mcr.EvaluateFunction("mymeshgrid", vectorstepx);
        }


      /// <summary>
      /// Provides a single output, 2-input interface to the mymeshgrid M-function.
      /// </summary>
      /// <remarks>
      /// M-Documentation:
      /// Using two colons to create a vector with increments between
      /// first and end elements.
      /// </remarks>
      /// <param name="vectorstepx">Input argument #1</param>
      /// <param name="vectorstepy">Input argument #2</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray mymeshgrid(MWArray vectorstepx, MWArray vectorstepy)
        {
          return mcr.EvaluateFunction("mymeshgrid", vectorstepx, vectorstepy);
        }


      /// <summary>
      /// Provides the standard 0-input interface to the mymeshgrid M-function.
      /// </summary>
      /// <remarks>
      /// M-Documentation:
      /// Using two colons to create a vector with increments between
      /// first and end elements.
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] mymeshgrid(int numArgsOut)
        {
          return mcr.EvaluateFunction(numArgsOut, "mymeshgrid");
        }


      /// <summary>
      /// Provides the standard 1-input interface to the mymeshgrid M-function.
      /// </summary>
      /// <remarks>
      /// M-Documentation:
      /// Using two colons to create a vector with increments between
      /// first and end elements.
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="vectorstepx">Input argument #1</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] mymeshgrid(int numArgsOut, MWArray vectorstepx)
        {
          return mcr.EvaluateFunction(numArgsOut, "mymeshgrid", vectorstepx);
        }


      /// <summary>
      /// Provides the standard 2-input interface to the mymeshgrid M-function.
      /// </summary>
      /// <remarks>
      /// M-Documentation:
      /// Using two colons to create a vector with increments between
      /// first and end elements.
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="vectorstepx">Input argument #1</param>
      /// <param name="vectorstepy">Input argument #2</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] mymeshgrid(int numArgsOut, MWArray vectorstepx,
                                  MWArray vectorstepy)
        {
          return mcr.EvaluateFunction(numArgsOut, "mymeshgrid",
                                      vectorstepx, vectorstepy);
        }


      /// <summary>
      /// Provides an interface for the mymeshgrid function in which the input and output
      /// arguments are specified as an array of MWArrays.
      /// </summary>
      /// <remarks>
      /// This method will allocate and return by reference the output argument
      /// array.<newpara></newpara>
      /// M-Documentation:
      /// Using two colons to create a vector with increments between
      /// first and end elements.
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return</param>
      /// <param name= "argsOut">Array of MWArray output arguments</param>
      /// <param name= "argsIn">Array of MWArray input arguments</param>
      ///
      public void mymeshgrid(int numArgsOut, ref MWArray[] argsOut, MWArray[] argsIn)
        {
          mcr.EvaluateFunction("mymeshgrid", numArgsOut, ref argsOut, argsIn);
        }


      /// <summary>
      /// Provides a single output, 0-input interface to the myplus M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray myplus()
        {
          return mcr.EvaluateFunction("myplus");
        }


      /// <summary>
      /// Provides a single output, 1-input interface to the myplus M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="a">Input argument #1</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray myplus(MWArray a)
        {
          return mcr.EvaluateFunction("myplus", a);
        }


      /// <summary>
      /// Provides a single output, 2-input interface to the myplus M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="a">Input argument #1</param>
      /// <param name="b">Input argument #2</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray myplus(MWArray a, MWArray b)
        {
          return mcr.EvaluateFunction("myplus", a, b);
        }


      /// <summary>
      /// Provides the standard 0-input interface to the myplus M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] myplus(int numArgsOut)
        {
          return mcr.EvaluateFunction(numArgsOut, "myplus");
        }


      /// <summary>
      /// Provides the standard 1-input interface to the myplus M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="a">Input argument #1</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] myplus(int numArgsOut, MWArray a)
        {
          return mcr.EvaluateFunction(numArgsOut, "myplus", a);
        }


      /// <summary>
      /// Provides the standard 2-input interface to the myplus M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="a">Input argument #1</param>
      /// <param name="b">Input argument #2</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] myplus(int numArgsOut, MWArray a, MWArray b)
        {
          return mcr.EvaluateFunction(numArgsOut, "myplus", a, b);
        }


      /// <summary>
      /// Provides an interface for the myplus function in which the input and output
      /// arguments are specified as an array of MWArrays.
      /// </summary>
      /// <remarks>
      /// This method will allocate and return by reference the output argument
      /// array.<newpara></newpara>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return</param>
      /// <param name= "argsOut">Array of MWArray output arguments</param>
      /// <param name= "argsIn">Array of MWArray input arguments</param>
      ///
      public void myplus(int numArgsOut, ref MWArray[] argsOut, MWArray[] argsIn)
        {
          mcr.EvaluateFunction("myplus", numArgsOut, ref argsOut, argsIn);
        }


      /// <summary>
      /// Provides a single output, 0-input interface to the mypolyfit M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray mypolyfit()
        {
          return mcr.EvaluateFunction("mypolyfit");
        }


      /// <summary>
      /// Provides a single output, 1-input interface to the mypolyfit M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="x">Input argument #1</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray mypolyfit(MWArray x)
        {
          return mcr.EvaluateFunction("mypolyfit", x);
        }


      /// <summary>
      /// Provides a single output, 2-input interface to the mypolyfit M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="x">Input argument #1</param>
      /// <param name="y">Input argument #2</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray mypolyfit(MWArray x, MWArray y)
        {
          return mcr.EvaluateFunction("mypolyfit", x, y);
        }


      /// <summary>
      /// Provides a single output, 3-input interface to the mypolyfit M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="x">Input argument #1</param>
      /// <param name="y">Input argument #2</param>
      /// <param name="n">Input argument #3</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray mypolyfit(MWArray x, MWArray y, MWArray n)
        {
          return mcr.EvaluateFunction("mypolyfit", x, y, n);
        }


      /// <summary>
      /// Provides the standard 0-input interface to the mypolyfit M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] mypolyfit(int numArgsOut)
        {
          return mcr.EvaluateFunction(numArgsOut, "mypolyfit");
        }


      /// <summary>
      /// Provides the standard 1-input interface to the mypolyfit M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="x">Input argument #1</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] mypolyfit(int numArgsOut, MWArray x)
        {
          return mcr.EvaluateFunction(numArgsOut, "mypolyfit", x);
        }


      /// <summary>
      /// Provides the standard 2-input interface to the mypolyfit M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="x">Input argument #1</param>
      /// <param name="y">Input argument #2</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] mypolyfit(int numArgsOut, MWArray x, MWArray y)
        {
          return mcr.EvaluateFunction(numArgsOut, "mypolyfit", x, y);
        }


      /// <summary>
      /// Provides the standard 3-input interface to the mypolyfit M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="x">Input argument #1</param>
      /// <param name="y">Input argument #2</param>
      /// <param name="n">Input argument #3</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] mypolyfit(int numArgsOut, MWArray x,
                                 MWArray y, MWArray n)
        {
          return mcr.EvaluateFunction(numArgsOut, "mypolyfit", x, y, n);
        }


      /// <summary>
      /// Provides an interface for the mypolyfit function in which the input and output
      /// arguments are specified as an array of MWArrays.
      /// </summary>
      /// <remarks>
      /// This method will allocate and return by reference the output argument
      /// array.<newpara></newpara>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return</param>
      /// <param name= "argsOut">Array of MWArray output arguments</param>
      /// <param name= "argsIn">Array of MWArray input arguments</param>
      ///
      public void mypolyfit(int numArgsOut, ref MWArray[] argsOut, MWArray[] argsIn)
        {
          mcr.EvaluateFunction("mypolyfit", numArgsOut, ref argsOut, argsIn);
        }


      /// <summary>
      /// Provides a single output, 0-input interface to the mypolyval M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray mypolyval()
        {
          return mcr.EvaluateFunction("mypolyval");
        }


      /// <summary>
      /// Provides a single output, 1-input interface to the mypolyval M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="p">Input argument #1</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray mypolyval(MWArray p)
        {
          return mcr.EvaluateFunction("mypolyval", p);
        }


      /// <summary>
      /// Provides a single output, 2-input interface to the mypolyval M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="p">Input argument #1</param>
      /// <param name="x">Input argument #2</param>
      /// <returns>An MWArray containing the first output argument.</returns>
      ///
      public MWArray mypolyval(MWArray p, MWArray x)
        {
          return mcr.EvaluateFunction("mypolyval", p, x);
        }


      /// <summary>
      /// Provides the standard 0-input interface to the mypolyval M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] mypolyval(int numArgsOut)
        {
          return mcr.EvaluateFunction(numArgsOut, "mypolyval");
        }


      /// <summary>
      /// Provides the standard 1-input interface to the mypolyval M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="p">Input argument #1</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] mypolyval(int numArgsOut, MWArray p)
        {
          return mcr.EvaluateFunction(numArgsOut, "mypolyval", p);
        }


      /// <summary>
      /// Provides the standard 2-input interface to the mypolyval M-function.
      /// </summary>
      /// <remarks>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return.</param>
      /// <param name="p">Input argument #1</param>
      /// <param name="x">Input argument #2</param>
      /// <returns>An Array of length "numArgsOut" containing the output
      /// arguments.</returns>
      ///
      public MWArray[] mypolyval(int numArgsOut, MWArray p, MWArray x)
        {
          return mcr.EvaluateFunction(numArgsOut, "mypolyval", p, x);
        }


      /// <summary>
      /// Provides an interface for the mypolyval function in which the input and output
      /// arguments are specified as an array of MWArrays.
      /// </summary>
      /// <remarks>
      /// This method will allocate and return by reference the output argument
      /// array.<newpara></newpara>
      /// </remarks>
      /// <param name="numArgsOut">The number of output arguments to return</param>
      /// <param name= "argsOut">Array of MWArray output arguments</param>
      /// <param name= "argsIn">Array of MWArray input arguments</param>
      ///
      public void mypolyval(int numArgsOut, ref MWArray[] argsOut, MWArray[] argsIn)
        {
          mcr.EvaluateFunction("mypolyval", numArgsOut, ref argsOut, argsIn);
        }


      /// <summary>
      /// This method will cause a MATLAB figure window to behave as a modal dialog box.
      /// The method will not return until all the figure windows associated with this
      /// component have been closed.
      /// </summary>
      /// <remarks>
      /// An application should only call this method when required to keep the
      /// MATLAB figure window from disappearing.  Other techniques, such as calling
      /// Console.ReadLine() from the application should be considered where
      /// possible.</remarks>
      ///
      public void WaitForFiguresToDie()
        {
          mcr.WaitForFiguresToDie();
        }


      
      #endregion Methods

      #region Class Members

      private static MWMCR mcr= null;

      private bool disposed= false;

      #endregion Class Members
    }
}
