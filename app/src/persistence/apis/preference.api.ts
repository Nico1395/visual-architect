import http from "@/http";
import type { PreferenceDtoV1, SetPreferenceDtoV1 } from "../dtos/preference.dtos";

export async function getPreferences() {
    const { data } = await http.get<Array<PreferenceDtoV1>>("/api/v1/preferences")
    return data
}

export async function setPreference(contract: SetPreferenceDtoV1) {
    await http.post("/api/v1/preferences/set", {
        key: contract.key,
        value: contract.value,
        resetToDefault: contract.resetToDefault
    })
}
