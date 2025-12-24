<script setup lang="ts">
import SettingsViewField from '../PreferencesViewField.vue';
import SettingsViewHeader from '../PreferencesViewHeader.vue';
import { Input } from '@/components/ui/input'
import { useI18n } from "vue-i18n"
import { useProfileStore } from '@/persistence/stores/profile.store';
import { storeToRefs } from 'pinia';
import { computed, reactive, watch } from 'vue';
import { toast } from 'vue-sonner'

const profileStore = useProfileStore();
const { t } = useI18n();
const { profile } = storeToRefs(profileStore)

const form = reactive({
  email: "",
  displayName: "",
})
const originalProfile = reactive({
  email: "",
  displayName: "",
})

const emailChanged = computed(() => form.email !== originalProfile.email)
const displayNameChanged = computed(() => form.displayName !== originalProfile.displayName)

async function saveChanges(promise: Promise<void>) {
    toast.promise(promise, {
        loading: t('toasts.saving.loading'),
        success: t('toasts.saving.success'),
        error: t('toasts.saving.error'),
    });

    await promise;
}

async function saveEmail() {
    await saveChanges(profileStore.saveProfile({ email: form.email, }))
    originalProfile.email = form.email
}

async function saveDisplayName() {
    await saveChanges(profileStore.saveProfile({ displayName: form.displayName, }))
    originalProfile.displayName = form.displayName
}

function resetField<K extends keyof typeof form>(key: K) {
  form[key] = originalProfile[key]
}

watch(profile, (p) => {
  if (!p)
    return

    Object.assign(originalProfile, {
        email: p.email,
        displayName: p.displayName,
    })

    Object.assign(form, {
        email: p.email,
        displayName: p.displayName,
    })
}, { immediate: true })
</script>

<template>
    <div class="profile-preferences">
        <div class="profile-preferences-header">
            <SettingsViewHeader>
                <template #preferences-header>
                    {{ t('preferences.profile.header') }}
                </template>

                <template #preferences-description>
                    {{ t('preferences.profile.description') }}
                </template>
            </SettingsViewHeader>
        </div>

        <SettingsViewField :disabled="profileStore.busy" :changed="emailChanged" @reset="resetField('email')" @save="saveEmail()" :grouped="true">
            <template #name>
                {{ t('preferences.profile.email') }}
            </template>

            <template #input>
                <Input
                    type="email"
                    length="256"
                    v-model="form.email"
                    :disabled="profileStore.busy"
                />
            </template>

            <template #desc>
                    {{ t('preferences.profile.emaildesc') }}
            </template>
        </SettingsViewField>

        <SettingsViewField :disabled="profileStore.busy" :changed="displayNameChanged" @reset="resetField('displayName')" @save="saveDisplayName()" :grouped="true">
            <template #name>
                {{ t('preferences.profile.displayname') }}
            </template>

            <template #input>
                <Input
                    type="text"
                    length="100"
                    v-model="form.displayName"
                    :disabled="profileStore.busy"
                />
            </template>
        </SettingsViewField>
    </div>
</template>

<style>
.profile-preferences {
    display: flex;
    flex-direction: column;
    gap: 2rem;
}
</style>
