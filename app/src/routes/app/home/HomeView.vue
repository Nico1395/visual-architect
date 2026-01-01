<script setup lang="ts">
import ViewMargin from "@/components/layout/ViewMargin.vue";
import HomeSection from "./HomeSection.vue";
import HomeProjectWidget from "./HomeProjectWidget.vue";
import type { DesignProjectDto } from "@/persistence/dtos/design-project.dtos";
import ButtonGroup from "@/components/ui/button-group/ButtonGroup.vue";
import Icon from "@/components/Icon.vue";
import Button from "@/components/ui/button/Button.vue";
import { useI18n } from "vue-i18n";
import DesignProjectFormDialog from "@/components/design-projects/DesignProjectFormDialog.vue";
import { ref } from "vue";
import { useRouter } from "vue-router";

const { t } = useI18n()
const router = useRouter()
const projectFormDialogOpened = ref(false)

const projects: Array<DesignProjectDto> = [
  {
    id: "proj_001",
    identityId: "user_123",
    name: "Personal Portfolio Website",
    descriptionPayload: "",
    createdAt: "2024-01-12T10:15:30.000Z",
    updatedAt: "2024-02-01T08:42:10.000Z",
  },
  {
    id: "proj_002",
    identityId: "user_123",
    name: "Task Management App",
    descriptionPayload: "",
    createdAt: "2024-02-05T14:22:00.000Z",
    updatedAt: "2024-02-20T16:10:45.000Z",
  },
  {
    id: "proj_003",
    identityId: "user_123",
    name: "E-commerce Storefront",
    descriptionPayload: "",
    createdAt: "2024-03-01T09:00:00.000Z",
    updatedAt: "2024-03-18T11:30:12.000Z",
  },
  {
    id: "proj_004",
    identityId: "user_123",
    name: "Internal Admin Dashboard",
    descriptionPayload: "",
    createdAt: "2024-03-22T17:45:10.000Z",
    updatedAt: "2024-04-02T13:05:55.000Z",
  },
  {
    id: "proj_005",
    identityId: "user_123",
    name: "Mobile Fitness Tracker",
    descriptionPayload: "",
    createdAt: "2024-04-10T07:30:00.000Z",
    updatedAt: "2024-04-25T18:20:40.000Z",
  },
  {
    id: "proj_006",
    identityId: "user_123",
    name: "Real-time Chat Application",
    descriptionPayload: "",
    createdAt: "2024-05-03T12:00:00.000Z",
    updatedAt: "2024-05-10T15:45:00.000Z",
  },
  {
    id: "proj_007",
    identityId: "user_123",
    name: "Analytics & Reporting Tool",
    descriptionPayload: "",
    createdAt: "2024-05-20T09:18:22.000Z",
    updatedAt: "2024-06-01T10:55:33.000Z",
  },
  {
    id: "proj_008",
    identityId: "user_123",
    name: "Marketing Landing Pages",
    descriptionPayload: "",
    createdAt: "2024-06-12T11:11:11.000Z",
    updatedAt: "2024-06-20T14:40:00.000Z",
  },
  {
    id: "proj_009",
    identityId: "user_123",
    name: "API Gateway Service",
    descriptionPayload: "",
    createdAt: "2024-07-01T08:00:00.000Z",
    updatedAt: "2024-07-09T09:35:50.000Z",
  },
  {
    id: "proj_010",
    identityId: "user_123",
    name: "Experimental AI Playground",
    descriptionPayload: "",
    createdAt: "2024-07-15T16:45:00.000Z",
    updatedAt: "2024-07-21T19:10:05.000Z",
  },
];

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
</script>

<template>
    <ViewMargin class="home-view">
        <HomeSection contentClass="home-project-overview">
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
                <HomeProjectWidget
                    v-for="project in projects"
                    :key="project.id"
                    :value="project"
                    :project="project" />
            </template>
        </HomeSection>
    </ViewMargin>

    <DesignProjectFormDialog
        v-model:opened="projectFormDialogOpened"
        @submitted="onProjectFormDialogSubmitted"
    />
</template>

<style>
.home-view {
    display: flex;
    flex-direction: column;
    gap: 5rem;

    .home-project-overview {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
        gap: 0.5rem;
    }
}
</style>
