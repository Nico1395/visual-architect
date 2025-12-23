import { defineStore } from "pinia";
import type { PreferenceDto } from "../dtos/preference.dtos";
import { getPreferences, setPreference } from "../apis/preference.api";

export const usePreferenceStore = defineStore("preference", {
    state: () => ({
        preferences: [] as Array<PreferenceDto>,
        busy: false,
    }),
    actions: {
        async getPreferences() {
            if (this.busy)
                return

            this.busy = true;

            try {
                this.preferences = await getPreferences()
            } catch (error) {
                console.error(error)
            } finally {
                this.busy = false
            }
        },
        async setPreference(key: string, value: string | null, resetToDefault: boolean = false) {
            const preference = this.preferences.find(p => p.key == key)
            if (!preference || this.busy)
                return

            this.busy = true;

            try {
                await setPreference({
                    key: key,
                    value: value,
                    resetToDefault: resetToDefault
                })

                preference.value = resetToDefault ? preference.defaultValue : value
            } catch (error) {
                console.error(error)
            } finally {
                this.busy = false
            }
        },
    }
})
