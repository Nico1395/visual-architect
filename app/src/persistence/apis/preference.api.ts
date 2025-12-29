import http from "@/http";
import type { PreferenceDto, SetPreferenceDto } from "../dtos/preference.dtos";

export async function getPreferences() {
    const { data } = await http.get<Array<PreferenceDto>>("/api/v1/preferences")
    return data
}

export async function setPreference(setPreference: SetPreferenceDto) {
    await http.post("/api/v1/preferences/set", {
        key: setPreference.key,
        value: setPreference.value,
        resetToDefault: setPreference.resetToDefault
    })
}
