import { defineStore } from "pinia";
import type { DesignProjectDto } from "../dtos/design-project.dtos";
import { addProject, getOwnedProjects, getProjectById, updateProject } from "../apis/design-project.api";

export const useDesignProjectStore = defineStore("design-project", {
    state: () => ({
        initialized: false,
        busy: false,
        projects: [] as Array<DesignProjectDto>,
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
        async updateProject(project: Partial<DesignProjectDto>) {
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
        }
    },
})
