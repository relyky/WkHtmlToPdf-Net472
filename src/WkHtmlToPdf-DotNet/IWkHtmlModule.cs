using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DinkToPdf
{
    public interface IWkHtmlModule
    {
        int wkhtmltopdf_extended_qt();

        IntPtr wkhtmltopdf_version();

        int wkhtmltopdf_init(int useGraphics);

        int wkhtmltopdf_deinit();

        IntPtr wkhtmltopdf_create_global_settings();

        int wkhtmltopdf_set_global_setting(IntPtr settings,
            [MarshalAs((short)CustomUnmanagedType.LPUTF8Str)]
            string name,
            [MarshalAs((short)CustomUnmanagedType.LPUTF8Str)]
            string value);

        int wkhtmltopdf_get_global_setting(IntPtr settings,
            [MarshalAs((short)CustomUnmanagedType.LPUTF8Str)]
            string name,
            byte[] array);

        int wkhtmltopdf_destroy_global_settings(IntPtr settings);

        IntPtr wkhtmltopdf_create_object_settings();

        int wkhtmltopdf_set_object_setting(IntPtr settings,
            [MarshalAs((short)CustomUnmanagedType.LPUTF8Str)]
            string name,
            [MarshalAs((short)CustomUnmanagedType.LPUTF8Str)]
            string value);

        int wkhtmltopdf_get_object_setting(IntPtr settings,
            [MarshalAs((short)CustomUnmanagedType.LPUTF8Str)]
            string name,
            byte[] array);

        int wkhtmltopdf_destroy_object_settings(IntPtr settings);

        IntPtr wkhtmltopdf_create_converter(IntPtr globalSettings);

        void wkhtmltopdf_add_object(IntPtr converter,
            IntPtr objectSettings,
            byte[] data);

        void wkhtmltopdf_add_object(IntPtr converter,
            IntPtr objectSettings,
            [MarshalAs((short)CustomUnmanagedType.LPUTF8Str)] string data);

        bool wkhtmltopdf_convert(IntPtr converter);

        void wkhtmltopdf_destroy_converter(IntPtr converter);

        int wkhtmltopdf_get_output(IntPtr converter, out IntPtr data);

        int wkhtmltopdf_set_phase_changed_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] VoidCallback callback);

        int wkhtmltopdf_set_progress_changed_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] VoidCallback callback);

        int wkhtmltopdf_set_finished_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] IntCallback callback);

        int wkhtmltopdf_set_warning_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] StringCallback callback);

        int wkhtmltopdf_set_error_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] StringCallback callback);

        int wkhtmltopdf_phase_count(IntPtr converter);

        int wkhtmltopdf_current_phase(IntPtr converter);

        IntPtr wkhtmltopdf_phase_description(IntPtr converter, int phase);

        IntPtr wkhtmltopdf_progress_string(IntPtr converter);

        int wkhtmltopdf_http_error_code(IntPtr converter);
    }
}
