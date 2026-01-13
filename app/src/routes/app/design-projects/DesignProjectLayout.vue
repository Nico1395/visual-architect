<script setup lang="ts">
import Card from '@/components/ui/card/Card.vue';
import DesignProjectMenuItem from './DesignProjectMenuItem.vue';
import { useI18n } from "vue-i18n"
import Icon from '@/components/Icon.vue';
import ViewMargin from '@/components/layout/ViewMargin.vue';
import DesignProjectMenuSeparator from './DesignProjectMenuSeparator.vue';
import { useRoute } from 'vue-router';
import { useDesignProjectStore } from '@/persistence/stores/design-project.store';
import { computed, provide, watch } from 'vue';

const { t } = useI18n();
const route = useRoute()
const projectId = computed(() => route.params.projectId as string)
const designProjectStore = useDesignProjectStore()

const project = computed(() =>
    designProjectStore.projects.find(p => p.id === projectId.value)
)

watch(
    () => projectId,
    (id) => designProjectStore.getProjectById(id.value),
    { immediate: true }
)

provide('design-project', project)
</script>

<template>
    <ViewMargin class="design-project-layout">
        <div class="design-project-header">
            <h1>
                {{ project?.name }}
            </h1>
        </div>

        <div class="design-project-body">
            <Card class="design-project-menu">
                <DesignProjectMenuItem :to="`/app/design-projects/${project?.id}/overview`">
                    <Icon icon="ai-map" />

                    {{ t('designprojects.overview.title') }}
                </DesignProjectMenuItem>

                <DesignProjectMenuItem :to="`/app/design-projects/${project?.id}/tasks`">
                    <Icon icon="bi bi-pass" />

                    {{ t('designprojects.tasks.title') }}
                </DesignProjectMenuItem>

                <DesignProjectMenuSeparator />

                <DesignProjectMenuItem :to="`/app/design-projects/${project?.id}/settings`">
                    <Icon icon="ai-gear" />

                    {{ t('designprojects.settings.title') }}
                </DesignProjectMenuItem>
            </Card>

            <div class="design-project-view">
                <RouterView />
            </div>
        </div>
    </ViewMargin>
</template>

<style>
.design-project-layout {
    display: flex;
    flex-direction: column;
    gap: 2rem;

    .design-project-header {
        flex: non;
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: center;

        > h1 {
            font-weight: 700;
            font-size: 26pt;
        }
    }

    .design-project-body {
        flex: 1;
        display: flex;
        flex-direction: row;
        gap: 3rem;

        .design-project-menu {
            width: 250px;
            flex: none;
            display: flex;
            flex-direction: column;
            gap: 0.2rem;
            padding: 1rem;
            height: fit-content;
        }

        .design-project-view {
            flex: 1;
        }
    }
}
</style>
