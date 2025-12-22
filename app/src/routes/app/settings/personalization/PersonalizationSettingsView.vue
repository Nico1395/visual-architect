<script setup lang="ts">
import SettingsViewHeader from '../SettingsViewHeader.vue';
import { useI18n } from "vue-i18n"

const { t } = useI18n();
</script>

<template>
    <div class="personalization-settings">
        <div class="personalization-settings-header">
            <SettingsViewHeader>
                <template #settings-header>
                    {{ t('settings.personalization.header') }}
                </template>

                <template #settings-description>
                    {{ t('settings.personalization.description') }}
                </template>
            </SettingsViewHeader>
        </div>

        test
    </div>
</template>

<style>
.personalization-settings {

}
</style>
