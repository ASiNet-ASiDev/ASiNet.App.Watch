using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace ASiNet.App.Watch;
public static class Win32Helper
{
    #region Window styles
    [Flags]
    public enum ExtendedWindowStyles : long
    {
        WS_EX_TOOLWINDOW = 0x00000080,
        WS_EX_NOACTIVATE = 0x08000000L,
    }

    public enum GetWindowLongFields
    {
        GWL_EXSTYLE = (-20),
    }

    [DllImport("user32.dll")]
    public static extern nint GetWindowLong(nint hWnd, int nIndex);

    public static nint SetWindowLong(nint hWnd, int nIndex, nint dwNewLong)
    {
        int error = 0;
        nint result = nint.Zero;
        SetLastError(0);

        if (nint.Size == 4)
        {
            int tempResult = IntSetWindowLong(hWnd, nIndex, IntPtrToInt32(dwNewLong));
            error = Marshal.GetLastWin32Error();
            result = new nint(tempResult);
        }
        else
        {
            result = IntSetWindowLongPtr(hWnd, nIndex, dwNewLong);
            error = Marshal.GetLastWin32Error();
        }

        if ((result == nint.Zero) && (error != 0))
        {
            throw new System.ComponentModel.Win32Exception(error);
        }

        return result;
    }

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(
    nint hWnd,
    nint hWndInsertAfter,
    int X,
    int Y,
    int cx,
    int cy,
    uint uFlags);

    const uint SWP_NOSIZE = 0x0001;
    const uint SWP_NOMOVE = 0x0002;
    const uint SWP_NOACTIVATE = 0x0010;

    static readonly nint HWND_BOTTOM = new(1);

    public static void SendWpfWindowBack(Window window)
    {
        var hWnd = new WindowInteropHelper(window).Handle;
        SetWindowPos(hWnd, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE);
    }

    public static void SendWpfWindowAsToolWindow(Window window)
    {
        var hWnd = new WindowInteropHelper(window).Handle;

        int exStyle = (int)GetWindowLong(hWnd, (int)GetWindowLongFields.GWL_EXSTYLE);
        exStyle |= (int)ExtendedWindowStyles.WS_EX_TOOLWINDOW;
        SetWindowLong(hWnd, (int)GetWindowLongFields.GWL_EXSTYLE, exStyle);
    }


    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
    private static extern nint IntSetWindowLongPtr(nint hWnd, int nIndex, nint dwNewLong);

    [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
    private static extern int IntSetWindowLong(nint hWnd, int nIndex, nint dwNewLong);

    private static int IntPtrToInt32(IntPtr intPtr)
    {
        return unchecked((int)intPtr.ToInt64());
    }

    [DllImport("kernel32.dll", EntryPoint = "SetLastError")]
    public static extern void SetLastError(int dwErrorCode);
    #endregion

}
