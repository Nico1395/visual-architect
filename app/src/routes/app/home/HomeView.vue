<script setup lang="ts">
import ViewMargin from "@/components/layout/ViewMargin.vue";
import HomeSection from "./HomeSection.vue";
import HomeProjectWidget from "./HomeProjectWidget.vue";
import ButtonGroup from "@/components/ui/button-group/ButtonGroup.vue";
import Icon from "@/components/Icon.vue";
import Button from "@/components/ui/button/Button.vue";
import { useI18n } from "vue-i18n";
import DesignProjectFormDialog from "@/components/design-projects/DesignProjectFormDialog.vue";
import { computed, onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { useDesignProjectStore } from "@/persistence/stores/design-project.store";
import Spinner from "@/components/ui/spinner/Spinner.vue";

const { t } = useI18n()
const router = useRouter()
const designProjectStore =  useDesignProjectStore()
const projectFormDialogOpened = ref(false)
const projects = computed(() => designProjectStore.projects)

function openProjectFormDialog() {
    projectFormDialogOpened.value = true
}

function onProjectFormDialogSubmitted(result: { projectId: string | null | undefined }) {
    if (!result.projectId)
        return

    router.push({
        path: `/app/design-projects/${result.projectId}`
    })
}

onMounted(() => {
    designProjectStore.getOwnedProjects()
})
</script>

<template>
    <ViewMargin class="home-view">
        <HomeSection contentClass="home-project-content">
            <template #title>
                {{ t('home.proj.title') }}
            </template>

            <template #actions>
                <ButtonGroup>
                    <Button variant="default" @click="openProjectFormDialog">
                        <Icon icon="ai-plus" />

                        {{ t('home.proj.new') }}
                    </Button>
                </ButtonGroup>
            </template>

            <template #description>
                This is a very very long description that hopefully will be long enough to actually take up some space and fill the screen.
            </template>

            <template #content>
                <div v-if="designProjectStore.busy" class="home-project-spinner">
                    <Spinner />

                    {{ t('home.proj.loading') }}
                </div>

                <div v-else-if="projects.length === 0" class="home-project-no-projects">
                    {{ t('home.proj.noproj') }}
                </div>

                <div v-else class="home-project-overview">
                    <HomeProjectWidget
                        v-for="project in projects"
                        :key="project.id"
                        :value="project"
                        :project="project" />
                </div>
            </template>
        </HomeSection>
    </ViewMargin>

    <DesignProjectFormDialog
        v-model:opened="projectFormDialogOpened"
        @submitted="onProjectFormDialogSubmitted"
    />
</template>

<style>
@keyframes spinnerBlink {
    0% {
        color: var(--foreground)
    }

    50% {
        color: var(--muted-foreground)
    }

    100% {
        color: var(--foreground)
    }
}

.home-view {
    display: flex;
    flex-direction: column;
    gap: 5rem;

    .home-project-content {
        .home-project-spinner, .home-project-no-projects {
            display: flex;
            flex-direction: row;
            gap: 0.5rem;
            justify-content: center;
            align-items: center;
            font-size: 10pt;
            font-weight: 500;
        }

        .home-project-spinner {
            animation: spinnerBlink 1s infinite;
        }

        .home-project-overview {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
            gap: 1.5rem;
        }
    }
}
</style>
