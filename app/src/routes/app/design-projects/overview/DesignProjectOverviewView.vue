<script setup lang="ts">
import MarkdownArea from '@/components/MarkdownArea.vue';
import type { DesignProjectDto } from '@/persistence/dtos/design-project.dtos';
import { useDesignProjectStore } from '@/persistence/stores/design-project.store';
import { inject, reactive, watch, type ComputedRef } from 'vue';
import { useI18n } from 'vue-i18n';
import { toast } from 'vue-sonner';

const { t } = useI18n();
const designProjectStore = useDesignProjectStore()
const project = inject<ComputedRef<DesignProjectDto | undefined>>('design-project')
if (!project) {
  throw new Error('DesignProject not provided')
}

const form = reactive({
    id: "",
    descriptionPayload: "",
})

async function saveDescriptionPayload() {
    await saveChanges(designProjectStore.updateProject({ id: form.id, descriptionPayload: form.descriptionPayload }))
}

async function saveChanges(promise: Promise<void>) {
    toast.promise(promise, {
        loading: t('toasts.saving.loading'),
        success: t('toasts.saving.success'),
        error: t('toasts.saving.error'),
    });

    await promise;
}

watch(project, (p) => {
        if (!p)
            return

        Object.assign(form, {
            id: project.value?.id,
            name: project.value?.name,
            descriptionPayload: project.value?.descriptionPayload
        })
    },
    { immediate: true }
)
</script>

<template>
    <div class="design-project-overview">
        <div class="design-project-overview-header">
            {{ t('designprojects.overview.title') }}
        </div>

        <div class="design-project-overview-content">
            <div class="design-project-overview-description">
                <div class="design-project-overview-description-header">
                    <div class="design-project-overview-description-title">
                        {{ t('designprojects.overview.description.title') }}
                    </div>
                </div>

                <div class="design-project-overview-description-body">
                    <MarkdownArea v-model="form.descriptionPayload" @save="saveDescriptionPayload" :placeholder="t('designprojects.overview.description.none')">
                        <template #title>
                            {{ t('designprojects.overview.description.editTitle') }}
                        </template>
                    </MarkdownArea>
                </div>
            </div>

            <div class="design-project-overview-task-stats">
                <div class="design-project-overview-task-stats-header">
                    <div class="design-project-overview-task-stats-title">
                        {{ t('designprojects.overview.taskStats.title') }}
                    </div>
                </div>

                <div class="design-project-overview-task-stats-content">
                    <div class="design-project-overview-task-stats-card">
                        TODO
                    </div>

                    <div class="design-project-overview-task-stats-card">
                        TODO
                    </div>

                    <div class="design-project-overview-task-stats-card">
                        TODO
                    </div>

                    <div class="design-project-overview-task-stats-card">
                        TODO
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style>
.design-project-overview {
    display: flex;
    flex-direction: column;
    gap: 1rem;

    .design-project-overview-header {
        font-weight: 700;
        font-size: 20pt;
    }

    .design-project-overview-content {
        display: flex;
        flex-direction: column;
        gap: 2rem;

        .design-project-overview-description {
            display: flex;
            flex-direction: column;
            gap: 0.5rem;

            .design-project-overview-description-header {
                display: flex;
                flex-direction: row;
                align-items: center;
                justify-content: space-between;

                .design-project-overview-description-title {
                    font-weight: 600;
                    font-size: 12pt;
                }
            }

            /* .design-project-overview-description-body {
            } */
        }

        .design-project-overview-task-stats {
            display: flex;
            flex-direction: column;
            gap: 0.5rem;

            .design-project-overview-task-stats-header {
                display: flex;
                flex-direction: row;
                align-items: center;
                justify-content: space-between;

                .design-project-overview-task-stats-title {
                    font-weight: 600;
                    font-size: 12pt;
                }
            }

            .design-project-overview-task-stats-content {
                display: grid;
                grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
                gap: 1.5rem;

                .design-project-overview-task-stats-card {
                    padding: 1rem;
                    background-color: var(--muted-background);
                    border-radius: var(--radius-xl);
                    aspect-ratio: 1;
                }
            }
        }
    }
}
</style>
