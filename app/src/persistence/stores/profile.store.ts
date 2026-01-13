import { defineStore } from "pinia";
import type { ProfileDto } from "../dtos/profile.dtos";
import { deleteProfile, getProfile, saveProfile } from "../apis/profile.api";

export const useProfileStore = defineStore("profile", {
    state: () => ({
        profile: null as ProfileDto | null,
        busy: false,
    }),
    actions: {
        async getProfile() {
            if (this.busy || this.profile)
                return this.profile;

            this.busy = true;

            try {
                this.profile = await getProfile()
            } catch (error) {
                console.error(error)
            } finally {
                this.busy = false;
                return this.profile;
            }
        },
        async saveProfile(profile: Partial<ProfileDto>) {
            if (this.busy || !this.profile)
                return;

            this.busy = true

            try {
                Object.assign(this.profile, profile)
                await saveProfile(this.profile)
            } catch (error) {
                console.error(error)
                throw error
            } finally {
                this.busy = false
            }
        },
        async deleteProfile() {
            if (this.busy || !this.profile)
                return;

            this.busy = true

            try {
                await deleteProfile()
                this.profile = null
            } catch (error) {
                console.error(error)
                throw error
            } finally {
                this.busy = false
            }
        }
    }
})
