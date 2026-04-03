using System.Runtime.CompilerServices;
using System.Text.Json;

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
        // All const string concatenation is folded at compile time → IL contains literal strings.
        // Type erasure: all ref-type args share via __Canon, so we use object where possible.
        // KeyValuePair<string, RefType> erases to KeyValuePair<__Canon, __Canon>
        //   → one call covers both KVP<string, JSComponentParameter[]> and KVP<string, List<string>>.
        // IEnumerable<KVP>/IReadOnlyCollection<KVP> share as TCollection via __Canon.
        // Enums/char/JsonElement are value types → each needs its own call.

        // Assemblies
        const string aCoreLib = "System.Private.CoreLib";
        const string aJson = "System.Text.Json";
        const string aRuntime = "System.Runtime";
        const string aJSInterop = "Microsoft.JSInterop";

        // Converter open generic type names
        const string tArrayConverter = "System.Text.Json.Serialization.Converters.ArrayConverter`2";
        const string tEnumConverter = "System.Text.Json.Serialization.Converters.EnumConverter`1";
        const string tObjectDefaultConverter = "System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1";
        const string tIEnumerableOfTConverter = "System.Text.Json.Serialization.Converters.IEnumerableOfTConverter`2";
        const string tICollectionOfTConverter = "System.Text.Json.Serialization.Converters.ICollectionOfTConverter`2";
        const string tKvp = "System.Collections.Generic.KeyValuePair`2";

        // Assembly-qualified type refs (for use as generic args)
        const string qObject = "System.Object, " + aCoreLib;
        const string qChar = "System.Char, " + aCoreLib;
        const string qJsonElement = "System.Text.Json.JsonElement, " + aJson;
        const string qJSCallResultType = "Microsoft.JSInterop.JSCallResultType, " + aJSInterop;
        const string qJSCallType = "Microsoft.JSInterop.Infrastructure.JSCallType, " + aJSInterop;

        // Composed assembly-qualified types
        const string qKvp = tKvp + "[[" + qObject + "],[" + qObject + "]], " + aCoreLib;
        const string qIEnumerableOfChar = "System.Collections.Generic.IEnumerable`1[[" + qChar + "]], " + aRuntime;
        const string qIEnumerableOfKvp = "System.Collections.Generic.IEnumerable`1[[" + qKvp + "]], " + aRuntime;
        const string qICollectionOfKvp = "System.Collections.Generic.ICollection`1[[" + qKvp + "]], " + aRuntime;

        // ArrayConverter<JsonElement[], JsonElement>
        CodeKeeper.Keep<JsonElement>();
        CodeKeeper.Keep(tArrayConverter + "[[" + qObject + "],[" + qJsonElement + "]], " + aJson);
        // EnumConverter<JSCallResultType>, EnumConverter<JSCallType>
        CodeKeeper.Keep(tEnumConverter + "[[" + qJSCallResultType + "]], " + aJson);
        CodeKeeper.Keep(tEnumConverter + "[[" + qJSCallType + "]], " + aJson);
        // ObjectDefaultConverter<KVP<object, object>>
        CodeKeeper.Keep(tObjectDefaultConverter + "[[" + qKvp + "]], " + aJson);
        // IEnumerableOfTConverter<IEnumerable<char>, char>
        CodeKeeper.Keep(tIEnumerableOfTConverter + "[[" + qIEnumerableOfChar + "],[" + qChar + "]], " + aJson);
        // IEnumerableOfTConverter<IEnumerable<KVP>, KVP>
        CodeKeeper.Keep(tIEnumerableOfTConverter + "[[" + qIEnumerableOfKvp + "],[" + qKvp + "]], " + aJson);
        // ICollectionOfTConverter<ICollection<KVP>, KVP>
        CodeKeeper.Keep(tICollectionOfTConverter + "[[" + qICollectionOfKvp + "],[" + qKvp + "]], " + aJson);
    }
}
