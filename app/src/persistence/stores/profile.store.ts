import { defineStore } from "pinia";
import type { ProfileDtoV1, UpdateProfileDtoV1 } from "../dtos/profile.dtos";
import { deleteProfile, getProfile, saveProfile } from "../apis/profile.api";

export const useProfileStore = defineStore("profile", {
    state: () => ({
        profile: null as ProfileDtoV1 | null,
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
        async saveProfile(contract: UpdateProfileDtoV1) {
            if (this.busy || !this.profile)
                return;

            this.busy = true

            try {
                await saveProfile(contract)

                this.profile.email = contract.email
                this.profile.displayName = contract.displayName
                this.profile.avatarUrl = contract.avatarUrl
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
