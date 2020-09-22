using System;
using System.Runtime.InteropServices;

namespace WkHtmlToPdfDotNet
{
    public class WkHtmlModule : IWkHtmlModule
    {
        public int wkhtmltopdf_extended_qt() => WkHtmlToXBindings.wkhtmltopdf_extended_qt();

        public IntPtr wkhtmltopdf_version() => WkHtmlToXBindings.wkhtmltopdf_version();

        public int wkhtmltopdf_init(int useGraphics) => WkHtmlToXBindings.wkhtmltopdf_init(useGraphics);

        public int wkhtmltopdf_deinit() => WkHtmlToXBindings.wkhtmltopdf_deinit();

        public IntPtr wkhtmltopdf_create_global_settings() => WkHtmlToXBindings.wkhtmltopdf_create_global_settings();

        public int wkhtmltopdf_set_global_setting(IntPtr settings,
            [MarshalAs((short)CustomUnmanagedType.LPUTF8Str)]
            string name,
            [MarshalAs((short)CustomUnmanagedType.LPUTF8Str)]
            string value) => WkHtmlToXBindings.wkhtmltopdf_set_global_setting(settings, name, value);

        public int wkhtmltopdf_get_global_setting(IntPtr settings,
            [MarshalAs((short)CustomUnmanagedType.LPUTF8Str)]
            string name,
            byte[] array)
        {
            int size = Marshal.SizeOf(array[0]) * array.Length;
            IntPtr pnt = Marshal.AllocHGlobal(size);

            try
            {
                // Copy the array to unmanaged memory.
                Marshal.Copy(array, 0, pnt, array.Length);

                return WkHtmlToXBindings.wkhtmltopdf_get_global_setting(settings, name, pnt, size);
            }
            finally
            {
                // Free the unmanaged memory.
                Marshal.FreeHGlobal(pnt);
            }
        }

        public int wkhtmltopdf_destroy_global_settings(IntPtr settings) => WkHtmlToXBindings.wkhtmltopdf_destroy_global_settings(settings);

        public IntPtr wkhtmltopdf_create_object_settings() => WkHtmlToXBindings.wkhtmltopdf_create_object_settings();

        public int wkhtmltopdf_set_object_setting(IntPtr settings,
            [MarshalAs((short)CustomUnmanagedType.LPUTF8Str)]
            string name,
            [MarshalAs((short)CustomUnmanagedType.LPUTF8Str)]
            string value) => WkHtmlToXBindings.wkhtmltopdf_set_object_setting(settings, name, value);

        public int wkhtmltopdf_get_object_setting(IntPtr settings,
            [MarshalAs((short)CustomUnmanagedType.LPUTF8Str)]
            string name,
            byte[] array)
        {
            int size = Marshal.SizeOf(array[0]) * array.Length;
            IntPtr pnt = Marshal.AllocHGlobal(size);

            try
            {
                // Copy the array to unmanaged memory.
                Marshal.Copy(array, 0, pnt, array.Length);

                return WkHtmlToXBindings.wkhtmltopdf_get_object_setting(settings, name, pnt, size);
            }
            finally
            {
                // Free the unmanaged memory.
                Marshal.FreeHGlobal(pnt);
            }
        }

        public int wkhtmltopdf_destroy_object_settings(IntPtr settings) => WkHtmlToXBindings.wkhtmltopdf_destroy_object_settings(settings);

        public IntPtr wkhtmltopdf_create_converter(IntPtr globalSettings) => WkHtmlToXBindings.wkhtmltopdf_create_converter(globalSettings);

        public void wkhtmltopdf_add_object(IntPtr converter,
            IntPtr objectSettings,
            byte[] data) => WkHtmlToXBindings.wkhtmltopdf_add_object(converter, objectSettings, data);

        public void wkhtmltopdf_add_object(IntPtr converter,
            IntPtr objectSettings,
            [MarshalAs((short)CustomUnmanagedType.LPUTF8Str)] string data) => WkHtmlToXBindings.wkhtmltopdf_add_object(converter, objectSettings, data);

        public bool wkhtmltopdf_convert(IntPtr converter) => WkHtmlToXBindings.wkhtmltopdf_convert(converter);

        public void wkhtmltopdf_destroy_converter(IntPtr converter) => WkHtmlToXBindings.wkhtmltopdf_destroy_converter(converter);

        public int wkhtmltopdf_get_output(IntPtr converter, out IntPtr data) => WkHtmlToXBindings.wkhtmltopdf_get_output(converter, out data);

        public int wkhtmltopdf_set_phase_changed_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] VoidCallback callback) => WkHtmlToXBindings.wkhtmltopdf_set_phase_changed_callback(converter, callback);

        public int wkhtmltopdf_set_progress_changed_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] VoidCallback callback) => WkHtmlToXBindings.wkhtmltopdf_set_progress_changed_callback(converter, callback);

        public int wkhtmltopdf_set_finished_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] IntCallback callback) => WkHtmlToXBindings.wkhtmltopdf_set_finished_callback(converter, callback);

        public int wkhtmltopdf_set_warning_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] StringCallback callback) => WkHtmlToXBindings.wkhtmltopdf_set_warning_callback(converter, callback);

        public int wkhtmltopdf_set_error_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] StringCallback callback) => WkHtmlToXBindings.wkhtmltopdf_set_error_callback(converter, callback);

        public int wkhtmltopdf_phase_count(IntPtr converter) => WkHtmlToXBindings.wkhtmltopdf_phase_count(converter);

        public int wkhtmltopdf_current_phase(IntPtr converter) => WkHtmlToXBindings.wkhtmltopdf_current_phase(converter);

        public IntPtr wkhtmltopdf_phase_description(IntPtr converter, int phase) => WkHtmlToXBindings.wkhtmltopdf_phase_description(converter, phase);

        public IntPtr wkhtmltopdf_progress_string(IntPtr converter) => WkHtmlToXBindings.wkhtmltopdf_progress_string(converter);

        public int wkhtmltopdf_http_error_code(IntPtr converter) => WkHtmlToXBindings.wkhtmltopdf_http_error_code(converter);
    }
}
