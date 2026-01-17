<script setup lang="ts">
import ViewMargin from '@/components/layout/ViewMargin.vue';
import { useI18n } from 'vue-i18n';
import AboutSection from './AboutSection.vue';

const { t } = useI18n()
</script>

<template>
    <ViewMargin class="about">
        <AboutSection>
            <template #header>
                {{ t('about.visualArchitect.title') }}
            </template>

            <template #body>
                <p>
                    {{ t('about.visualArchitect.project') }}
                </p>
            </template>
        </AboutSection>

        <AboutSection>
            <template #header>
                {{ t('about.privacy.title') }}
            </template>

            <template #body>
                {{ t('about.privacy.body') }}
            </template>
        </AboutSection>

        <AboutSection>
            <template #header>
                {{ t('about.impress.title') }}
            </template>

            <template #body>
                {{ t('about.impress.body') }}
            </template>
        </AboutSection>
    </ViewMargin>
</template>

<style>
.about {
    display: flex;
    flex-direction: column;
    gap: 3rem;
}
</style>
