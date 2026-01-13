<script setup lang="ts">
import type { DesignProjectDto } from '@/persistence/dtos/design-project.dtos';
import { inject, type ComputedRef } from 'vue';
import { useI18n } from 'vue-i18n';
import VueMarkdown from 'vue-markdown-render'

const { t } = useI18n();
const project = inject<ComputedRef<DesignProjectDto | undefined>>('design-project')
if (!project) {
  throw new Error('DesignProject not provided')
}
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

                    <!-- <ButtonGroup>
                        <Button variant="outline" size="sm">
                            {{ t('designprojects.overview.descriptionEdit') }}

                            <Icon icon="ai-pencil" />
                        </Button>
                    </ButtonGroup> -->
                </div>

                <div class="design-project-overview-description-body">
                    <div v-if="!project?.descriptionPayload" class="design-project-overview-description-nodesc">
                        {{ t('designprojects.overview.description.none') }}
                    </div>

                    <VueMarkdown class="markdown" v-else :source="project?.descriptionPayload ?? ''" />
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

            .design-project-overview-description-body {
                padding: 1.5rem;
                background-color: var(--muted-background);
                border-radius: var(--radius-xl);

                .design-project-overview-description-nodesc {
                    font-weight: 500;
                    font-size: 10pt;
                    color: var(--muted-foreground);
                    text-align: center;
                    margin: 1rem auto;
                }
            }
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
