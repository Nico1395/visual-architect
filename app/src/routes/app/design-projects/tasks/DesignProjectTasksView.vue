<script setup lang="ts">
import Icon from '@/components/Icon.vue';
import ButtonGroup from '@/components/ui/button-group/ButtonGroup.vue';
import Button from '@/components/ui/button/Button.vue';
import { useI18n } from "vue-i18n"
import { inject, type ComputedRef } from 'vue'
import type { DesignProjectDto, DesignTaskDto } from '@/persistence/dtos/design-project.dtos';
import DesignProjectTaskFilter from './DesignProjectTaskFilter.vue';
import DesignProjectTaskItem from './DesignProjectTaskItem.vue';

const { t } = useI18n();
const project = inject<ComputedRef<DesignProjectDto | undefined>>('design-project')
if (!project) {
  throw new Error('DesignProject not provided')
}

const dummyDesignTasks: DesignTaskDto[] = [
  {
    id: "task-001",
    projectId: "project-123",
    number: 1,
    name: "Wireframe Landing Page",
    descriptionPayload: "Create low-fidelity wireframes for the main landing page.",
    status: 0,
    designs: null,
    createdAt: "2026-01-01T09:00:00Z",
    updatedAt: "2026-01-01T09:00:00Z"
  },
  {
    id: "task-002",
    projectId: "project-123",
    number: 2,
    name: "Homepage Visual Design",
    descriptionPayload: "Design high-fidelity visuals for the homepage based on approved wireframes.",
    status: 1,
    designs: null,
    createdAt: "2026-01-02T09:00:00Z",
    updatedAt: "2026-01-03T10:30:00Z"
  },
  {
    id: "task-003",
    projectId: "project-123",
    number: 3,
    name: "Design System Setup",
    descriptionPayload: "Define color palette, typography, spacing, and core UI components.",
    status: 2,
    designs: null,
    createdAt: "2026-01-03T09:00:00Z",
    updatedAt: "2026-01-05T14:15:00Z"
  },
  {
    id: "task-004",
    projectId: "project-123",
    number: 4,
    name: "Mobile Navigation Design",
    descriptionPayload: "Design responsive mobile navigation patterns and interactions.",
    status: 1,
    designs: null,
    createdAt: "2026-01-04T09:00:00Z",
    updatedAt: "2026-01-06T11:45:00Z"
  },
  {
    id: "task-005",
    projectId: "project-123",
    number: 5,
    name: "Dashboard Layout",
    descriptionPayload: "Create layout options for the main user dashboard.",
    status: 0,
    designs: null,
    createdAt: "2026-01-05T09:00:00Z",
    updatedAt: "2026-01-05T09:00:00Z"
  },
  {
    id: "task-006",
    projectId: "project-123",
    number: 6,
    name: "User Profile Screen",
    descriptionPayload: "Design user profile and account settings screens.",
    status: 1,
    designs: null,
    createdAt: "2026-01-06T09:00:00Z",
    updatedAt: "2026-01-07T13:20:00Z"
  },
  {
    id: "task-007",
    projectId: "project-123",
    number: 7,
    name: "Empty States & Error Screens",
    descriptionPayload: "Design empty, loading, and error states for key flows.",
    status: 0,
    designs: null,
    createdAt: "2026-01-07T09:00:00Z",
    updatedAt: "2026-01-07T09:00:00Z"
  },
  {
    id: "task-008",
    projectId: "project-123",
    number: 8,
    name: "Icon Set Creation",
    descriptionPayload: "Create a custom icon set aligned with the brand style.",
    status: 2,
    designs: null,
    createdAt: "2026-01-08T09:00:00Z",
    updatedAt: "2026-01-09T16:40:00Z"
  },
  {
    id: "task-009",
    projectId: "project-123",
    number: 9,
    name: "Prototype Interactions",
    descriptionPayload: "Build interactive prototypes for key user journeys.",
    status: 1,
    designs: null,
    createdAt: "2026-01-09T09:00:00Z",
    updatedAt: "2026-01-10T12:10:00Z"
  },
  {
    id: "task-010",
    projectId: "project-123",
    number: 10,
    name: "Design Handoff Preparation",
    descriptionPayload: "Prepare final design files and specs for developer handoff.",
    status: 0,
    designs: null,
    createdAt: "2026-01-10T09:00:00Z",
    updatedAt: "2026-01-10T09:00:00Z"
  }
]
</script>

<template>
    <div class="design-project-tasks">
        <div class="design-project-tasks-header">
            <div class="design-project-tasks-title">
                <h1>
                    {{ t('designprojects.tasks.title') }}
                </h1>
            </div>

            <div class="design-project-tasks-actions">
                <DesignProjectTaskFilter />

                <ButtonGroup>
                    <Button>
                        <Icon icon="ai-plus" />

                        {{ t('designprojects.tasks.new') }}
                    </Button>
                </ButtonGroup>
            </div>
        </div>

        <div class="design-project-tasks-body">
            <div v-if="dummyDesignTasks.length == 0" class="design-project-tasks-none">
                {{ t('designprojects.tasks.list.notasks') }}
            </div>

            <div v-else class="design-project-tasks-items">
                <DesignProjectTaskItem
                    v-for="task in dummyDesignTasks"
                    :key="task.id"
                    :value="task"
                    :task="task" />
            </div>
        </div>
    </div>
</template>

<style>
.design-project-tasks {
    display: flex;
    flex-direction: column;
    gap: 1rem;

    .design-project-tasks-header {
        flex: none;
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: center;
        gap: 1rem;
        flex-wrap: wrap;

        .design-project-tasks-title {
            > h1 {
                font-weight: 700;
                font-size: 20pt;
            }
        }

        .design-project-tasks-actions {
            display: flex;
            flex-direction: row;
            gap: 0.5rem;
        }
    }

    .design-project-tasks-body {
        .design-project-tasks-none {
            font-weight: 500;
            font-size: 10pt;
            color: var(--muted-foreground);
            text-align: center;
            margin-top: 1rem;
        }

        .design-project-tasks-items {
            display: flex;
            flex-direction: column;
            gap: 0.2rem;
        }
    }
}
</style>
