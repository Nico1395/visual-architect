<script setup lang="ts">
import SettingsViewField from '../SettingsViewField.vue';
import SettingsViewHeader from '../SettingsViewHeader.vue';
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
        loading: t('settings.profile.toast.loading'),
        success: t('settings.profile.toast.success'),
        error: t('settings.profile.toast.error'),
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
    <div class="profile-settings">
        <div class="profile-settings-header">
            <SettingsViewHeader>
                <template #settings-header>
                    {{ t('settings.profile.header') }}
                </template>

                <template #settings-description>
                    {{ t('settings.profile.description') }}
                </template>
            </SettingsViewHeader>
        </div>

        <SettingsViewField :disabled="profileStore.busy" :changed="emailChanged" @reset="resetField('email')" @save="saveEmail()" :grouped="true">
            <template #name>
                {{ t('settings.profile.email') }}
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
                    {{ t('settings.profile.emaildesc') }}
            </template>
        </SettingsViewField>

        <SettingsViewField :disabled="profileStore.busy" :changed="displayNameChanged" @reset="resetField('displayName')" @save="saveDisplayName()" :grouped="true">
            <template #name>
                {{ t('settings.profile.displayname') }}
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
.profile-settings {
    display: flex;
    flex-direction: column;
    gap: 2rem;
}
</style>
