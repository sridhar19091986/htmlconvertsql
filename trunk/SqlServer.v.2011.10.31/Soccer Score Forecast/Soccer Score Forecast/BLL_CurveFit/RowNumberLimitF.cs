using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Soccer_Score_Forecast
{
    public partial class RowNumberLimit : IDisposable
    {

        #region dispose

        // Pointer to an external unmanaged resource.
        private IntPtr handle;
        // Other managed resource this class uses.
        // private Component Components;
        // Track whether Dispose has been called.
        private bool disposed = false;

        // Constructor for the BaseResource object.
        //public BaseResource()
        //{
        //   // Insert appropriate constructor code here.
        //}

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // Take yourself off the Finalization queue 
            // to prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the 
        // runtime from inside the finalizer and you should not reference 
        // other objects. Only unmanaged resources can be disposed.
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                //if (disposing)
                //{
                //    // Dispose managed resources.
                //    Components.Dispose();
                //}
                // Release unmanaged resources. If disposing is false, 
                // only the following code is executed.
                CloseHandle(handle);
                handle = IntPtr.Zero;
                // Note that this is not thread safe.
                // Another thread could start disposing the object
                // after the managed resources are disposed,
                // but before the disposed flag is set to true.
                // If thread safety is necessary, it must be
                // implemented by the client.

            }
            disposed = true;
        }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method 
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        //~BaseResource()      
        ~RowNumberLimit()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        // Allow your Dispose method to be called multiple times,
        // but throw an exception if the object has been disposed.
        // Whenever you do something with this class, 
        // check to see if it has been disposed.
        public void DoSomething()
        {
            if (this.disposed)
            {
                //throw new ObjectDisposedException();
            }
        }


        // Design pattern for a derived class.
        // Note that this derived class inherently implements the 
        // IDisposable interface because it is implemented in the base class.


        public void Close()
        {
            // Calls the Dispose method without parameters.
            Dispose();
        }

        #endregion
    }
}
