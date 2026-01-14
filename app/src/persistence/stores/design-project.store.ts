import { defineStore } from "pinia";
import type { DesignProjectDtoV1 } from "../dtos/design-project.dtos";
import { addProject, addTask, deleteProject, getOwnedProjects, getProjectById, updateProject } from "../apis/design-project.api";

export const useDesignProjectStore = defineStore("design-project", {
    state: () => ({
        initialized: false,
        busy: false,
        projects: [] as Array<DesignProjectDtoV1>,
    }),
    actions: {
        async getOwnedProjects() {
            if (this.busy)
                return;

            this.busy = true

            try {
                return this.projects = await getOwnedProjects()
            } catch (error) {
                console.error(error)
            } finally {
                this.busy = false
            }
        },
        async getProjectById(projectId: string) {
            if (this.busy)
                return;

            this.busy = true

            try {
                let project = this.projects.find(p => p.id === projectId);
                if (!project) {
                    project = await getProjectById(projectId)
                    if (project) {
                        this.projects.push(project)
                    }
                }

                return project
            } catch (error) {
                console.error(error)
            } finally {
                this.busy = false
            }
        },
        async addProject(name: string, description: string | null) {
            if (this.busy)
                return null;

            this.busy = true

            try {
                return await addProject({
                    name: name,
                    descriptionPayload: description,
                })
            } catch (error) {
                console.error(error)
                throw error
            } finally {
                this.busy = false
            }
        },
        async updateProject(project: Partial<DesignProjectDtoV1>) {
            if (this.busy)
                return;

            this.busy = true

            try {
                const cached = this.projects.find(p => p.id == project?.id);
                if (!cached)
                    throw new Error("The project to update should be cached")

                Object.assign(cached, project)
                await updateProject(cached)
            } catch (error) {
                console.error(error)
                throw error
            } finally {
                this.busy = false
            }
        },
        async deleteProject(projectId: string) {
            if (this.busy)
                return;

            this.busy = true

            try {
                const cached = this.projects.find(p => p.id == projectId);
                if (cached)
                    this.projects = this.projects.filter(p => p != cached)      // Remove the project from cache

                await deleteProject(projectId)
            } catch (error) {
                console.error(error)
                throw error
            } finally {
                this.busy = false
            }
        },
        async addTask(projectId: string, name: string, description: string) {
            if (this.busy)
                return null;

            this.busy = true

            try {
                return await addTask(projectId, {
                    name: name,
                    descriptionPayload: description,
                })
            } catch (error) {
                console.error(error)
                throw error
            } finally {
                this.busy = false
            }
        }
    },
})
