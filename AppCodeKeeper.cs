using System.Runtime.CompilerServices;

namespace MauiNativeAotQuirks;

public static class AppCodeKeeper
{
    [ModuleInitializer]
    public static void Use()
    {
        // Preserve Blazor component constructors for ActivatorUtilities
        CodeKeeper.Keep<Components.Routes>();
        CodeKeeper.Keep<Components.Pages.Home>();
        CodeKeeper.Keep<Components.Pages.Counter>();
        CodeKeeper.Keep<Components.Pages.Weather>();
        CodeKeeper.Keep<Components.Pages.NotFound>();
        CodeKeeper.Keep<Components.Layout.MainLayout>();
        CodeKeeper.Keep<Components.Layout.NavMenu>();

        // BlazorWebView IPC JSON serialization types (string-based Keep for internal types)

        // ArrayConverter for IpcCommon's JsonElement[] deserialization
        CodeKeeper.Keep("System.Text.Json.Serialization.Converters.ArrayConverter`2[[System.Text.Json.JsonElement[], System.Text.Json],[System.Text.Json.JsonElement, System.Text.Json]], System.Text.Json");

        // EnumConverters
        CodeKeeper.Keep("System.Text.Json.Serialization.Converters.EnumConverter`1[[Microsoft.JSInterop.JSCallResultType, Microsoft.JSInterop]], System.Text.Json");
        CodeKeeper.Keep("System.Text.Json.Serialization.Converters.EnumConverter`1[[Microsoft.JSInterop.Infrastructure.JSCallType, Microsoft.JSInterop]], System.Text.Json");

        // ObjectDefaultConverter for KeyValuePair types
        CodeKeeper.Keep("System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1[[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[Microsoft.AspNetCore.Components.Web.JSComponentConfigurationStore+JSComponentParameter[], Microsoft.AspNetCore.Components.Web]], System.Private.CoreLib]], System.Text.Json");
        CodeKeeper.Keep("System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1[[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[System.Collections.Generic.List`1[[System.String, System.Private.CoreLib]], System.Private.CoreLib]], System.Private.CoreLib]], System.Text.Json");

        // IEnumerableOfTConverter for IEnumerable<char>
        CodeKeeper.Keep("System.Text.Json.Serialization.Converters.IEnumerableOfTConverter`2[[System.Collections.Generic.IEnumerable`1[[System.Char, System.Private.CoreLib]], System.Runtime],[System.Char, System.Private.CoreLib]], System.Text.Json");

        // Converters for IEnumerable/IReadOnlyCollection/ICollection of KeyValuePair<string, JSComponentParameter[]>
        CodeKeeper.Keep("System.Text.Json.Serialization.Converters.IEnumerableOfTConverter`2[[System.Collections.Generic.IEnumerable`1[[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[Microsoft.AspNetCore.Components.Web.JSComponentConfigurationStore+JSComponentParameter[], Microsoft.AspNetCore.Components.Web]], System.Private.CoreLib]], System.Runtime],[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[Microsoft.AspNetCore.Components.Web.JSComponentConfigurationStore+JSComponentParameter[], Microsoft.AspNetCore.Components.Web]], System.Private.CoreLib]], System.Text.Json");
        CodeKeeper.Keep("System.Text.Json.Serialization.Converters.IEnumerableOfTConverter`2[[System.Collections.Generic.IReadOnlyCollection`1[[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[Microsoft.AspNetCore.Components.Web.JSComponentConfigurationStore+JSComponentParameter[], Microsoft.AspNetCore.Components.Web]], System.Private.CoreLib]], System.Runtime],[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[Microsoft.AspNetCore.Components.Web.JSComponentConfigurationStore+JSComponentParameter[], Microsoft.AspNetCore.Components.Web]], System.Private.CoreLib]], System.Text.Json");
        CodeKeeper.Keep("System.Text.Json.Serialization.Converters.ICollectionOfTConverter`2[[System.Collections.Generic.ICollection`1[[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[Microsoft.AspNetCore.Components.Web.JSComponentConfigurationStore+JSComponentParameter[], Microsoft.AspNetCore.Components.Web]], System.Private.CoreLib]], System.Runtime],[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[Microsoft.AspNetCore.Components.Web.JSComponentConfigurationStore+JSComponentParameter[], Microsoft.AspNetCore.Components.Web]], System.Private.CoreLib]], System.Text.Json");

        // Converters for IEnumerable/IReadOnlyCollection/ICollection of KeyValuePair<string, List<string>>
        CodeKeeper.Keep("System.Text.Json.Serialization.Converters.IEnumerableOfTConverter`2[[System.Collections.Generic.IEnumerable`1[[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[System.Collections.Generic.List`1[[System.String, System.Private.CoreLib]], System.Private.CoreLib]], System.Private.CoreLib]], System.Runtime],[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[System.Collections.Generic.List`1[[System.String, System.Private.CoreLib]], System.Private.CoreLib]], System.Private.CoreLib]], System.Text.Json");
        CodeKeeper.Keep("System.Text.Json.Serialization.Converters.IEnumerableOfTConverter`2[[System.Collections.Generic.IReadOnlyCollection`1[[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[System.Collections.Generic.List`1[[System.String, System.Private.CoreLib]], System.Private.CoreLib]], System.Private.CoreLib]], System.Runtime],[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[System.Collections.Generic.List`1[[System.String, System.Private.CoreLib]], System.Private.CoreLib]], System.Private.CoreLib]], System.Text.Json");
        CodeKeeper.Keep("System.Text.Json.Serialization.Converters.ICollectionOfTConverter`2[[System.Collections.Generic.ICollection`1[[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[System.Collections.Generic.List`1[[System.String, System.Private.CoreLib]], System.Private.CoreLib]], System.Private.CoreLib]], System.Runtime],[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[System.Collections.Generic.List`1[[System.String, System.Private.CoreLib]], System.Private.CoreLib]], System.Private.CoreLib]], System.Text.Json");
    }
}
