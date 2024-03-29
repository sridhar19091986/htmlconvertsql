MATLAB Deployment Checklist
===========================

Use this checklist to successfully deploy your application, component, 
or library.

You can distribute a MATLAB Compiler-generated standalone
application, component, or library to any target machine with 
the same operating system as the machine on which 
the application was compiled (the "source" development machine).

For more information, see the documentation for the MATLAB Compiler at:
http://www.mathworks.com/access/helpdesk/help/toolbox/compiler/

NOTE: Replace the items enclosed in angle brackets ( < > )
      with your own values.

1. Ensure that the MATLAB Component 
   Runtime (MCR) is installed on target machines, and ensure you
   have installed the correct version. To verify this, do the following: 
	
	A. Attempt to verify the MCR exists on your system.
         The MCR usually resides in these locations on these platforms:   
	 
        Operating System                Path to MCR
        ================                ===========
        Windows            C:\Program Files\MATLAB\MATLAB Component Runtime\v77
        Mac/Intel Mac      Applications/MATLAB/MATLAB_Component_Runtime/v77 
        Linux              *** No predetermined installation location ***
                           Contact your system administrator.  

         *NOTE: For information about the installation process and the MCR, 
                type "help MCR" or "help mcrinstaller" at the MATLAB command prompt. 
     
      B. MATLAB 2007b uses MCR version 7.7. To verify
         the version number of the installed MCR, type the following command:
            [mcrmajor,mcrminor]=mcrversion
         at the MATLAB command prompt.
	 
						
2. Add the MCR directory to the path specified by the target system's 
   environment variable. 
	
	A. Locate the name of the environment variable to set, using the 
         table below:  

              Operating System      Environment Variable
              ================      ====================
                 Windows               PATH           
                 Linux	               LD_LIBRARY_PATH
                 Solaris               LD_LIBRARY_PATH    
                 Mac                   DYLD_LIBRARY_PATH                 

 	B. Set the path by doing one of the following:

		On Windows systems:

		* Add the MCR directory to the environment variable by opening a 
		  command prompt and issuing the DOS command, specifying either win32 or win64:

  			set PATH=C:\Program Files\MATLAB\MATLAB Component
			Runtime\v77\runtime\{win32|win64};%PATH% 

                   Alternately, for Windows, add the following pathname:
                        C:\Program Files\MATLAB\MATLAB Component Runtime\v77\runtime\{win32|win64}
                   to the PATH environment variable, specifying either win32 or win64,
                   by doing the following:
                        1. Select the My Computer icon on your desktop.
                        2. Right-click the icon and select Properties from the menu.
                        3. Select the Advanced tab.
                        4. Click Environment Variables.  

		On UNIX systems:
		
		* Add the MCR directory to the environment variable by issuing 
		  the following commands:
 	
                 Linux
                       setenv LD_LIBRARY_PATH
                          <mcr_root>/v77/runtime/glnx86:
                          <mcr_root>/v77/sys/os/glnx86:
                          <mcr_root>/v77/sys/java/jre/glnx86/jre1.6.0/lib/i386/native_threads:
                          <mcr_root>/v77/sys/java/jre/glnx86/jre1.6.0/lib/i386/server:
                          <mcr_root>/v77/sys/java/jre/glnx86/jre1.6.0/lib/i386
                       setenv XAPPLRESDIR <mcr_root>/v77/X11/app-defaults

                 Solaris64
                       setenv LD_LIBRARY_PATH /usr/lib/lwp:
                          <mcr_root>/v77/runtime/sol64:
                          <mcr_root>/v77/sys/os/sol64:
                          <mcr_root>/v77/sys/java/jre/sol64/jre1.6.0/lib/sparcv9/native_threads:
                          <mcr_root>/v77/sys/java/jre/sol64/jre1.6.0/lib/sparcv9/server:
                          <mcr_root>/v77/sys/java/jre/sol64/jre1.6.0/lib/sparcv9
                       setenv XAPPLRESDIR <mcr_root>/v77/X11/app-defaults

                 Linux x86-64
                       setenv LD_LIBRARY_PATH
                          <mcr_root>/v77/runtime/glnxa64:
                          <mcr_root>/v77/sys/os/glnxa64:
                          <mcr_root>/v77/sys/java/jre/glnxa64/jre1.6.0/lib/amd64/native_threads:
                          <mcr_root>/v77/sys/java/jre/glnxa64/jre1.6.0/lib/amd64/server:
                          <mcr_root>/v77/sys/java/jre/glnxa64/jre1.6.0/lib/amd64 
                       setenv XAPPLRESDIR <mcr_root>/v77/X11/app-defaults

                 Mac OS X
                       setenv DYLD_LIBRARY_PATH
                          <mcr_root>/v77/runtime/mac:
                          <mcr_root>/v77/sys/os/mac:
                          <mcr_root>/v77/bin/mac:
                          /System/Library/Frameworks/JavaVM.framework/JavaVM:
                          /System/Library/Frameworks/JavaVM.framework/Libraries
                       setenv XAPPLRESDIR <mcr_root>/v77/X11/app-defaults

                 Intel Mac (Maci)
                       setenv DYLD_LIBRARY_PATH
                          <mcr_root>/version/runtime/maci:
                          <mcr_root>/version/sys/os/maci:
                          <mcr_root>/version/bin/maci:
                          /System/Library/Frameworks/JavaVM.framework/JavaVM:
                          /System/Library/Frameworks/JavaVM.framework/Libraries
                       setenv XAPPLRESDIR <mcr_root>/version/X11/app-defaults

		  
         NOTE: To make theses changes persistent after logout on UNIX or Mac machines, 
               modify the .cshrc file to include this setenv command.        
         NOTE: On Windows, the Environment Variable syntax utilizes 
               backslashes (\), delimited by semi-colons (;). On UNIX,
               the EV syntax utilizes forward slashes (/), delimited by colons (:). 
         NOTE: When deploying C and C++ standalone applications, it is possible
               to run the shell script file run_CurveFitCom.sh on UNIX and Mac
               instead of setting environment variables. See "Requirements
               for Standalone Applications," below.    
	

3. Collect the following files, based on what you are deploying: 

       Requirements for Standalone Applications:
       =========================================
  
	All Platforms:
		* CurveFitCom.ctf (Component Technology File archive,
		  platform-dependent on target machine)
			
        Windows:
		* MCRInstaller.exe (self-extracting MATLAB Component Runtime
		  library installer)   
		* CurveFitCom.exe (application created by MATLAB Compiler)
        UNIX:
		* MCRInstaller.bin (MATLAB Component Runtime library 
	 	  installer)
		* CurveFitCom (application created by MATLAB Compiler)
		* run_CurveFitCom.sh (shell script that can be run 
	 	  to temporarily set environment paths and execute the application)
        Mac:
		* MATLAB_Component_Runtime.dmg (MATLAB Component Runtime library 
	 	  installer)
		* CurveFitCom (application created by MATLAB Compiler)
		* run_CurveFitCom.sh (shell script that can be run 
	 	  to temporarily set environment paths and execute the application)

        

 	
       Requirements for C and C++ shared libraries:
       ============================================================

	All Platforms:            
		* CurveFitCom.dll (shared library, file extension 
		  varies by platform)
		* CurveFitCom.h (library header file)
		* CurveFitCom.ctf (Component Technology File archive,
		  platform-dependent on target machine, contains compiled M-code)
	
        Windows:
		* MCRInstaller.exe (self-extracting MATLAB Component Runtime
		  library installer)  
		
        UNIX:
		* MCRInstaller.bin (MATLAB Component Runtime library 
	 	  installer)
				
        Mac:
		* MATLAB_Component_Runtime.dmg (MATLAB Component Runtime library 
	 	  installer)
				
	
	

       Requirements for .NET Components (Windows only):
       ================================================

	* CurveFitCom.ctf (Component Technology File archive, 
	  contains compiled M-code)	
	* CurveFitCom.xml (documentation files)
	* CurveFitCom.pdb (program database file - if 
	  DEBUG option selected)
	* CurveFitCom.dll (component assembly file)
	* MCRInstaller.exe (self-extracting MATLAB Component Runtime
	  library archive)  
		

       Requirements for COM Components (Windows only):
       ===============================================

	* CurveFitCom.ctf (Component Technology File archive, 
	  contains compiled M-code)
	* CurveFitCom_0_0.dll (COM component)
	* MCRInstaller.exe (self-extracting MATLAB Component Runtime
	  library archive)  	
		

       Requirements for Java Components (All Platforms):
       =================================================

	* CurveFitCom.jar (Java class files and compiled M-code)

        Windows:
		* MCRInstaller.exe (self-extracting MATLAB Component Runtime
		  library installer)  
		
        UNIX:
		* MCRInstaller.bin (MATLAB Component Runtime library 
	 	  installer)
				
        Mac:
		* MATLAB_Component_Runtime.dmg (MATLAB Component Runtime library 
	 	  installer)


       Requirements for COM Components for Use with Microsoft Excel (Windows only):
       ============================================================================

	* CurveFitCom_0_0.dll (COM component)
	* CurveFitCom.ctf (Component Technology File archive, 
	  contains compiled M-code)
	* CurveFitCom.xla (Optionally created and placed in
	  d:\My Documents\MATLAB\CurveFitCom\src directory by the user)  
	* MCRInstaller.exe (self-extracting MATLAB Component Runtime
	  library archive)  	

	
