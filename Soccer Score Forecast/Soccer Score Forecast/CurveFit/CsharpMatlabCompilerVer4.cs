using System;
using System.Runtime.InteropServices;

namespace UtilityMatlabCompilerVer4
{
    class MatlabCSharp // for MATLAB 7 and MATLAB Compiler 4
    {
        [DllImport("libmx.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr mxCreateDoubleMatrix(int m, int n, mxComplexity flag);
        public enum mxComplexity
        { mxREAL, mxCOMPLEX };
        [DllImport("libmx.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void mxSetPr(IntPtr pa, IntPtr preal);
        [DllImport("libmx.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void mxSetPi(IntPtr pa, IntPtr pimag);
        [DllImport("libmx.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mxGetM(IntPtr pa);
        [DllImport("libmx.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void mxSetM(IntPtr pa, int m);
        [DllImport("libmx.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mxGetN(IntPtr pa);
        [DllImport("libmx.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void mxSetN(IntPtr pa, int n);
        [DllImport("libmx.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr mxGetPr(IntPtr pa);
        [DllImport("libmx.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr mxGetPi(IntPtr pa);
        [DllImport("libmx.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr mxDuplicateArray(IntPtr pa);
        [DllImport("libmx.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr mxCreateString(String str);
        // DLL for MATLAB Engine
        [DllImport("libeng.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr engOpen(string startMATLAB);
        [DllImport("libeng.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int engClose(IntPtr ep);
        [DllImport("libeng.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int engPutVariable(IntPtr ep, string ML_name, IntPtr mx_name);
        [DllImport("libeng.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int engEvalString(IntPtr ep, string ML_command);
        [DllImport("libeng.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr engGetVariable(IntPtr ep, string ML_resultName);
        // DLL for MATLAB Engine -- End
        // SCALAR
        public static IntPtr double2mxArray_scalarReal(double dbScalar)
        {
            // convert a double scalar to an mxArray mxPointer
            double[] dbBuffer;
            dbBuffer = new double[1];
            IntPtr mxPointer = (IntPtr)0;
            IntPtr mxPointerBuffer = (IntPtr)0;
            IntPtr mxPointerTemp;
            try
            {
                mxPointerBuffer = mxCreateDoubleMatrix(1, 1, MatlabCSharp.mxComplexity.mxREAL);
            }
            catch
            {
                System.Console.WriteLine("We cannot create a pointer of mxArray. Please check again.");
                return (IntPtr)0;
            }
            dbBuffer[0] = dbScalar;
            mxPointerTemp = Marshal.AllocHGlobal(dbBuffer.Length * Marshal.SizeOf(dbBuffer[0]));
            Marshal.Copy(dbBuffer, 0, mxPointerTemp, 1);
            mxSetPr(mxPointerBuffer, mxPointerTemp);
            mxPointer = mxDuplicateArray(mxPointerBuffer);
            Marshal.FreeHGlobal(mxPointerTemp);
            return mxPointer;
        }
        public static IntPtr double2mxArray_scalarComplex(double dbScalarReal, double dbScalarImag)
        {
            // convert double scalars to a complex mxArray mxPointer
            double[] dbBufferReal;
            dbBufferReal = new double[1];
            double[] dbBufferImag;
            dbBufferImag = new double[1];
            IntPtr mxPointer = (IntPtr)0;
            IntPtr mxPointerBuffer = (IntPtr)0;
            IntPtr mxPointerTempReal;
            IntPtr mxPointerTempImag;
            try
            {
                mxPointerBuffer = mxCreateDoubleMatrix(1, 1, MatlabCSharp.mxComplexity.mxCOMPLEX);
            }
            catch
            {
                System.Console.WriteLine("We cannot create a pointer of mxArray. Please check again.");
                return (IntPtr)0;
            }
            dbBufferReal[0] = dbScalarReal;
            dbBufferImag[0] = dbScalarImag;
            mxPointerTempReal = Marshal.AllocHGlobal(dbBufferReal.Length * Marshal.SizeOf(dbBufferReal[0]));
            mxPointerTempImag = Marshal.AllocHGlobal(dbBufferImag.Length * Marshal.SizeOf(dbBufferImag[0]));
            Marshal.Copy(dbBufferReal, 0, mxPointerTempReal, 1);
            Marshal.Copy(dbBufferImag, 0, mxPointerTempImag, 1);
            mxSetPr(mxPointerBuffer, mxPointerTempReal);
            mxSetPi(mxPointerBuffer, mxPointerTempImag);
            mxPointer = mxDuplicateArray(mxPointerBuffer);
            Marshal.FreeHGlobal(mxPointerTempReal);
            Marshal.FreeHGlobal(mxPointerTempImag);
            return mxPointer;
        }
        public static double mxArray2double_scalarReal(IntPtr mxPointer)
        {
            // convert a real term of an mxArray mxPointer to a double scalar
            double dbScalar;
            IntPtr PointerRealScalar;
            double[] dbVector;
            dbVector = new double[1];
            PointerRealScalar = mxGetPr(mxPointer);
            Marshal.Copy(PointerRealScalar, dbVector, 0, 1);
            dbScalar = dbVector[0];
            return dbScalar;
        }
        public static void mxArray2double_scalarComplex(
        IntPtr mxPointer, ref double dbReal, ref double dbImag)
        {
            // convert a complex mxArray mxPointer to double scalars
            IntPtr PointerScalarReal;
            IntPtr PointerScalarImag = (IntPtr)null;
            // for real term
            double[] dbVectorReal;
            dbVectorReal = new double[1];
            PointerScalarReal = mxGetPr(mxPointer);
            Marshal.Copy(PointerScalarReal, dbVectorReal, 0, 1);
            dbReal = dbVectorReal[0];
            // for imaginary term
            double[] dbVectorImag;
            dbVectorImag = new double[1];
            try
            {
                PointerScalarImag = mxGetPi(mxPointer);
                Marshal.Copy(PointerScalarImag, dbVectorImag, 0, 1);
                dbImag = dbVectorImag[0];
            }
            catch
            {
                dbImag = 0.0;
            }
        }
        // VECTOR
        public static IntPtr double2mxArray_vectorColumnReal(double[] dbVector)
        {
            // convert a double vector to an mxArray mxPointer
            // transfer to an mxArray vector with number of col = 1
            int vectorSize;
            vectorSize = dbVector.GetUpperBound(0) - dbVector.GetLowerBound(0) + 1;
            IntPtr mxPointer = (IntPtr)0;
            IntPtr mxPointerBuffer = (IntPtr)0;
            IntPtr mxPointerTemp;
            int row = vectorSize;
            double[] dbVectorBuffer;
            dbVectorBuffer = new double[row];
            try
            {
                mxPointerBuffer = mxCreateDoubleMatrix(row, 1, MatlabCSharp.mxComplexity.mxREAL);
            }
            catch
            {
                System.Console.Write("We cannot create a pointer mxArray. Please check again.");
                return (IntPtr)0;
            }
            mxPointerTemp = Marshal.AllocHGlobal(dbVector.Length * Marshal.SizeOf(dbVector[0]));
            Marshal.Copy(dbVector, 0, mxPointerTemp, dbVector.Length);
            mxSetPr(mxPointerBuffer, mxPointerTemp);
            mxPointer = mxDuplicateArray(mxPointerBuffer);
            Marshal.FreeHGlobal(mxPointerTemp);
            return mxPointer;
        }
        public static IntPtr double2mxArray_vectorRowReal(double[] dbVector)
        {
            // convert a double vector to an mxArray mxPointer
            // transfer to an mxArray vector with number of row = 1
            int vectorSize;
            vectorSize = dbVector.GetUpperBound(0) - dbVector.GetLowerBound(0) + 1;
            IntPtr mxPointer = (IntPtr)0;
            IntPtr mxPointerBuffer = (IntPtr)0;
            IntPtr mxPointerTemp;
            int col = vectorSize;
            double[] dbVectorBuffer;
            dbVectorBuffer = new double[col];
            try
            {
                mxPointerBuffer = mxCreateDoubleMatrix(1, col, MatlabCSharp.mxComplexity.mxREAL);
            }
            catch
            {
                System.Console.Write("We cannot create a pointer mxArray. Please check again.");
                return (IntPtr)0;
            }
            mxPointerTemp = Marshal.AllocHGlobal(dbVector.Length * Marshal.SizeOf(dbVector[0]));
            Marshal.Copy(dbVector, 0, mxPointerTemp, dbVector.Length);
            mxSetPr(mxPointerBuffer, mxPointerTemp);
            mxPointer = mxDuplicateArray(mxPointerBuffer);
            Marshal.FreeHGlobal(mxPointerTemp);
            return mxPointer;
        }
        public static IntPtr double2mxArray_vectorColumnComplex(
        double[] dbVectorReal, double[] dbVectorImag)
        {
            // convert double vectors to a complex mxArray mxPointer
            // transfer to an mxArray vector with number of col = 1
            int vectorSize;
            vectorSize = dbVectorReal.GetUpperBound(0) - dbVectorReal.GetLowerBound(0) + 1;
            IntPtr mxPointer = (IntPtr)0;
            IntPtr mxPointerBuffer = (IntPtr)0;
            IntPtr mxPointerTempReal;
            IntPtr mxPointerTempImag;
            int row = vectorSize;
            double[] dbVectorBuffer;
            dbVectorBuffer = new double[row];
            try
            {
                mxPointerBuffer = mxCreateDoubleMatrix(row, 1, MatlabCSharp.mxComplexity.mxCOMPLEX);
            }
            catch
            {
                System.Console.Write("We cannot create a pointer mxArray. Please check again.");
                return (IntPtr)0;
            }
            mxPointerTempReal = Marshal.AllocHGlobal(dbVectorReal.Length * Marshal.SizeOf(dbVectorReal[0]));
            mxPointerTempImag = Marshal.AllocHGlobal(dbVectorImag.Length * Marshal.SizeOf(dbVectorImag[0]));
            Marshal.Copy(dbVectorReal, 0, mxPointerTempReal, dbVectorReal.Length);
            Marshal.Copy(dbVectorImag, 0, mxPointerTempImag, dbVectorImag.Length);
            mxSetPr(mxPointerBuffer, mxPointerTempReal);
            mxSetPi(mxPointerBuffer, mxPointerTempImag);
            mxPointer = mxDuplicateArray(mxPointerBuffer);
            Marshal.FreeHGlobal(mxPointerTempReal);
            Marshal.FreeHGlobal(mxPointerTempImag);
            return mxPointer;
        }
        public static IntPtr double2mxArray_vectorRowComplex(double[] dbVectorReal, double[] dbVectorImag)
        {
            // convert double vectors to a complex mxArray mxPointer
            // transfer to an mxArray vector with number of row = 1
            int vectorSize;
            vectorSize = dbVectorReal.GetUpperBound(0) - dbVectorReal.GetLowerBound(0) + 1;
            IntPtr mxPointer = (IntPtr)0;
            IntPtr mxPointerBuffer = (IntPtr)0;
            IntPtr mxPointerTempReal;
            IntPtr mxPointerTempImag;
            int col = vectorSize;
            double[] dbVectorBuffer;
            dbVectorBuffer = new double[col];
            try
            {
                mxPointerBuffer = mxCreateDoubleMatrix(1, col, MatlabCSharp.mxComplexity.mxCOMPLEX);
            }
            catch
            {
                System.Console.Write("We cannot create a pointer mxArray. Please check again.");
                return (IntPtr)0;
            }
            mxPointerTempReal = Marshal.AllocHGlobal(dbVectorReal.Length * Marshal.SizeOf(dbVectorReal[0]));
            mxPointerTempImag = Marshal.AllocHGlobal(dbVectorImag.Length * Marshal.SizeOf(dbVectorImag[0]));
            Marshal.Copy(dbVectorReal, 0, mxPointerTempReal, dbVectorReal.Length);
            Marshal.Copy(dbVectorImag, 0, mxPointerTempImag, dbVectorImag.Length);
            mxSetPr(mxPointerBuffer, mxPointerTempReal);
            mxSetPi(mxPointerBuffer, mxPointerTempImag);
            mxPointer = mxDuplicateArray(mxPointerBuffer);
            Marshal.FreeHGlobal(mxPointerTempReal);
            Marshal.FreeHGlobal(mxPointerTempImag);
            return mxPointer;
        }
        public static double[] mxArray2double_vectorReal(IntPtr mxPointer)
        {
            // convert a real term of an mxArray mxPointer to a double vector
            int vectorSize;
            int row = mxGetM(mxPointer);
            int col = mxGetN(mxPointer);
            if (row > col) { vectorSize = row; }
            else { vectorSize = col; }
            IntPtr PointerVector;
            double[] dbVector;
            dbVector = new double[vectorSize];
            PointerVector = mxGetPr(mxPointer);
            Marshal.Copy(PointerVector, dbVector, 0, vectorSize);
            return dbVector;
        }
        public static void mxArray2double_vectorComplex(
        IntPtr mxPointer, ref double[] dbVectorReal, ref double[] dbVectorImag)
        {
            // convert a complex mxArray mxPointer to double vectors
            int vectorSize;
            int row = mxGetM(mxPointer);
            int col = mxGetN(mxPointer);
            if (row > col) { vectorSize = row; }
            else { vectorSize = col; }
            // for real term
            IntPtr PointerVectorReal;
            PointerVectorReal = mxGetPr(mxPointer);
            Marshal.Copy(PointerVectorReal, dbVectorReal, 0, vectorSize);
            // for imaginary term
            IntPtr PointerVectorImag;
            int i;
            try
            {
                PointerVectorImag = mxGetPi(mxPointer);
                Marshal.Copy(PointerVectorImag, dbVectorImag, 0, vectorSize);
            }
            catch
            {
                for (i = 0; i < vectorSize; i++)
                {
                    dbVectorImag[i] = 0.0;
                }
            }
        }
        // MATRIX
        public static IntPtr double2mxArray_matrixReal(double[,] dbMatrix)
        {
            // convert a double matrix to an mxArray mxPointer
            int i, j, row, col, index;
            row = dbMatrix.GetUpperBound(0) - dbMatrix.GetLowerBound(0) + 1;
            col = dbMatrix.GetUpperBound(1) - dbMatrix.GetLowerBound(1) + 1;
            IntPtr mxPointer = (IntPtr)0;
            IntPtr mxPointerBuffer = (IntPtr)0;
            IntPtr mxPointerTemp;
            try
            {
                mxPointerBuffer = mxCreateDoubleMatrix(row, col, MatlabCSharp.mxComplexity.mxREAL);
            }
            catch
            {
                System.Console.Write("We cannot create a pointer mxArray. Please check again.");
                return (IntPtr)0;
            }
            double[] dbVectorBuffer;
            dbVectorBuffer = new double[row * col];
            //transfer double matrix to double vector
            for (i = 0; i < row; i++)
            {
                for (j = 0; j < col; j++)
                {
                    index = j * row + i;
                    dbVectorBuffer[index] = dbMatrix[i, j];
                }
            }
            mxPointerTemp = Marshal.AllocHGlobal(dbVectorBuffer.Length * Marshal.SizeOf(dbVectorBuffer[0]));
            Marshal.Copy(dbVectorBuffer, 0, mxPointerTemp, dbVectorBuffer.Length);
            mxSetPr(mxPointerBuffer, mxPointerTemp);
            mxPointer = mxDuplicateArray(mxPointerBuffer);
            Marshal.FreeHGlobal(mxPointerTemp);
            return mxPointer;
        }
        public static IntPtr double2mxArray_matrixComplex(double[,] dbMatrixReal, double[,] dbMatrixImag)
        {
            // convert a double matrixes to a complex mxArray mxPointer
            int i, j, row, col, index;
            row = dbMatrixReal.GetUpperBound(0) - dbMatrixReal.GetLowerBound(0) + 1;
            col = dbMatrixReal.GetUpperBound(1) - dbMatrixReal.GetLowerBound(1) + 1;
            IntPtr mxPointer = (IntPtr)0;
            IntPtr mxPointerBuffer = (IntPtr)0;
            IntPtr mxPointerTempReal;
            IntPtr mxPointerTempImag;
            try
            {
                mxPointerBuffer = mxCreateDoubleMatrix(row, col, MatlabCSharp.mxComplexity.mxCOMPLEX);
            }
            catch
            {
                System.Console.Write("We cannot create a pointer mxArray. Please check again.");
                return (IntPtr)0;
            }
            double[] dbVectorBufferReal;
            dbVectorBufferReal = new double[row * col];
            double[] dbVectorBufferImag;
            dbVectorBufferImag = new double[row * col];
            //transfer double matrix to double vector
            for (i = 0; i < row; i++)
            {
                for (j = 0; j < col; j++)
                {
                    index = j * row + i;
                    dbVectorBufferReal[index] = dbMatrixReal[i, j];
                    dbVectorBufferImag[index] = dbMatrixImag[i, j];
                }
            }
            mxPointerTempReal = Marshal.AllocHGlobal(
            dbVectorBufferReal.Length * Marshal.SizeOf(dbVectorBufferReal[0]));
            mxPointerTempImag = Marshal.AllocHGlobal(
            dbVectorBufferImag.Length * Marshal.SizeOf(dbVectorBufferImag[0]));
            Marshal.Copy(dbVectorBufferReal, 0, mxPointerTempReal, dbVectorBufferReal.Length);
            Marshal.Copy(dbVectorBufferImag, 0, mxPointerTempImag, dbVectorBufferImag.Length);
            mxSetPr(mxPointerBuffer, mxPointerTempReal);
            mxSetPi(mxPointerBuffer, mxPointerTempImag);
            mxPointer = mxDuplicateArray(mxPointerBuffer);
            Marshal.FreeHGlobal(mxPointerTempReal);
            Marshal.FreeHGlobal(mxPointerTempImag);
            return mxPointer;
        }
        public static double[,] mxArray2double_matrixReal(IntPtr mxPointer)
        {
            // convert a real term of an mxArray mxPointer to a double matrix
            int i, j, index, row, col;
            row = mxGetM(mxPointer);
            col = mxGetN(mxPointer);
            IntPtr PointerDbArray;
            double[,] dbRealMatrix;
            dbRealMatrix = new double[row, col];
            PointerDbArray = mxGetPr(mxPointer);
            double[] dbBufferVector;
            dbBufferVector = new double[row * col];
            Marshal.Copy(PointerDbArray, dbBufferVector, 0, row * col);
            // convert vector to matrix
            for (i = 0; i < row; i++)
            {
                for (j = 0; j < col; j++)
                {
                    index = j * row + i;
                    dbRealMatrix[i, j] = dbBufferVector[index];
                }
            }
            return dbRealMatrix;
        }
        public static void mxArray2double_matrixComplex(
        IntPtr mxPointer, ref double[,] dbMatrixReal, ref double[,] dbMatrixImag)
        {
            // convert a complex mxArray mxPointer to double matrixes
            int i, j, index, row, col;
            row = mxGetM(mxPointer);
            col = mxGetN(mxPointer);
            // for real term
            IntPtr PointerDbArrayReal;
            PointerDbArrayReal = mxGetPr(mxPointer);
            double[] dbBufferVectorReal;
            dbBufferVectorReal = new double[row * col];
            Marshal.Copy(PointerDbArrayReal, dbBufferVectorReal, 0, row * col);
            // convert vector to matrix
            for (i = 0; i < row; i++)
            {
                for (j = 0; j < col; j++)
                {
                    index = j * row + i;
                    dbMatrixReal[i, j] = dbBufferVectorReal[index];
                }
            }
            // for imaginary term
            IntPtr PointerDbArrayImag;
            try
            {
                PointerDbArrayImag = mxGetPi(mxPointer);
                double[] dbBufferVectorImag;
                dbBufferVectorImag = new double[row * col];
                Marshal.Copy(PointerDbArrayImag, dbBufferVectorImag, 0, row * col);
                // convert vector to matrix
                for (i = 0; i < row; i++)
                {
                    for (j = 0; j < col; j++)
                    {
                        index = j * row + i;
                        dbMatrixImag[i, j] = dbBufferVectorImag[index];
                    }
                }
            }
            catch
            {
                for (i = 0; i < row; i++)
                {
                    for (j = 0; j < col; j++)
                    {
                        dbMatrixImag[i, j] = 0.0;
                    }
                }
            }
        }
        /* ******************************** */
        /* ******************************** */
        /* ******************************** */
        public static void printMatrix(double[,] matrix)
        {
            int i, j, row, col;
            row = matrix.GetUpperBound(0) - matrix.GetLowerBound(0) + 1;
            col = matrix.GetUpperBound(1) - matrix.GetLowerBound(1) + 1;
            for (i = 0; i < row; i++)
            {
                for (j = 0; j < col; j++)
                {
                    Console.Write("{0} \t", matrix.GetValue(i, j).ToString());
                }
                Console.WriteLine();
            }
        }
        /* ******************************** */
        public static void printVector(double[] vector)
        {
            int i, row;
            row = vector.GetUpperBound(0) - vector.GetLowerBound(0) + 1;
            for (i = 0; i < row; i++)
            {
                Console.Write("{0} \t", vector.GetValue(i).ToString());
                Console.WriteLine();
            }
        }
        /* ******************************** */
        /* ******************************** */
        /* ******************************** */
    } // end class
}
