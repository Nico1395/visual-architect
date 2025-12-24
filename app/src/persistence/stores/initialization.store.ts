import { defineStore } from "pinia";
import { usePreferenceStore } from "./preference.store";
import { useProfileStore } from "./profile.store";

export type ColorMode = "light" | "dark" | "auto"

export function mapToColorMode(value: string | null): ColorMode {
    if (value === "light" || value === "dark") {
        return value
    }

    if (value === "system" || value === "auto") {
        return "auto"
    }

    return "light"
}

export const useInitializationStore = defineStore("initialization", {
    state: () => ({
        initialized: false,
        busy: false
    }),
    actions: {
        async initialize() {
            if (this.busy)
                return;

            this.busy = true

            const profileStore = useProfileStore()
            await profileStore.getProfile()

            const preferenceStore = usePreferenceStore()
            await preferenceStore.getPreferences()

            this.initialized = true
            this.busy = false
        }
    },
})
