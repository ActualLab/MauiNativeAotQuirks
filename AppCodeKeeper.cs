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

        // BlazorWebView IPC JSON serialization types (string-based Keep for internal types).
        // Type erasure notes:
        // - All ref-type args share via __Canon, so we use object where possible
        // - KeyValuePair<string, RefType> erases to KeyValuePair<__Canon, __Canon>
        //   → one call covers both KVP<string, JSComponentParameter[]> and KVP<string, List<string>>
        // - IEnumerable<KVP>/IReadOnlyCollection<KVP>/ICollection<KVP> are all ref types
        //   → share via __Canon as TCollection, so one IEnumerableOfTConverter call covers all variants
        // - Enums and char are value types → each needs its own call

        // ArrayConverter: JsonElement is a struct, needs specific instantiation
        CodeKeeper.Keep("System.Text.Json.Serialization.Converters.ArrayConverter`2[[System.Text.Json.JsonElement[], System.Text.Json],[System.Text.Json.JsonElement, System.Text.Json]], System.Text.Json");

        // EnumConverters: enums are value types, each needs specific instantiation
        CodeKeeper.Keep("System.Text.Json.Serialization.Converters.EnumConverter`1[[Microsoft.JSInterop.JSCallResultType, Microsoft.JSInterop]], System.Text.Json");
        CodeKeeper.Keep("System.Text.Json.Serialization.Converters.EnumConverter`1[[Microsoft.JSInterop.Infrastructure.JSCallType, Microsoft.JSInterop]], System.Text.Json");

        // ObjectDefaultConverter for KeyValuePair<string, object>: covers all KVP<string, RefType> via __Canon
        CodeKeeper.Keep("System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1[[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[System.Object, System.Private.CoreLib]], System.Private.CoreLib]], System.Text.Json");

        // IEnumerableOfTConverter for IEnumerable<char>: char is a value type
        CodeKeeper.Keep("System.Text.Json.Serialization.Converters.IEnumerableOfTConverter`2[[System.Collections.Generic.IEnumerable`1[[System.Char, System.Private.CoreLib]], System.Runtime],[System.Char, System.Private.CoreLib]], System.Text.Json");

        // IEnumerableOfTConverter for KVP<string, object>: covers IEnumerable/IReadOnlyCollection variants
        // for both KVP<string, JSComponentParameter[]> and KVP<string, List<string>> via __Canon
        CodeKeeper.Keep("System.Text.Json.Serialization.Converters.IEnumerableOfTConverter`2[[System.Collections.Generic.IEnumerable`1[[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[System.Object, System.Private.CoreLib]], System.Private.CoreLib]], System.Runtime],[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[System.Object, System.Private.CoreLib]], System.Private.CoreLib]], System.Text.Json");

        // ICollectionOfTConverter for KVP<string, object>: same __Canon reasoning
        CodeKeeper.Keep("System.Text.Json.Serialization.Converters.ICollectionOfTConverter`2[[System.Collections.Generic.ICollection`1[[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[System.Object, System.Private.CoreLib]], System.Private.CoreLib]], System.Runtime],[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[System.Object, System.Private.CoreLib]], System.Private.CoreLib]], System.Text.Json");
    }
}
