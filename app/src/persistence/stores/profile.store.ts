import { defineStore } from "pinia";
import type { ProfileDto } from "../dtos/profile.dtos";
import { getProfile } from "../apis/profile.api";

export const useProfileStore = defineStore("profile", {
    state: () => ({
        profile: null as ProfileDto | null,
        loading: false
    }),
    actions: {
        async getProfile() {
            if (this.loading || this.profile)
                return this.profile;

            this.loading = true;

            try {
                this.profile = await getProfile()
            } catch (error) {
                console.error(error)
            } finally {
                this.loading = false;
                return this.profile;
            }
        }
    }
})
