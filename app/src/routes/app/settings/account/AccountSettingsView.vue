<script setup lang="ts">
import SettingsViewHeader from '../SettingsViewHeader.vue';
import { useI18n } from "vue-i18n"

const { t } = useI18n();
</script>

<template>
    <div class="account-settings">
        <div class="account-settings-header">
            <SettingsViewHeader>
                <template #settings-header>
                    {{ t('settings.account.header') }}
                </template>

                <template #settings-description>
                    {{ t('settings.account.description') }}
                </template>
            </SettingsViewHeader>
        </div>

        test
    </div>
</template>

<style>
.account-settings {

}
</style>
