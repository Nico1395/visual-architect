import { defineStore } from "pinia";
import type { DesignProjectDtoV1, UpdateDesignTaskDtoV1 } from "../dtos/design-project.dtos";
import { addProject, addTask, deleteProject, deleteTask, getOwnedProjects, getProjectById, getTaskByProjectIdAndNumber, updateProject, updateTask } from "../apis/design-project.api";

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
                await deleteProject(projectId)

                if (cached)
                    this.projects = this.projects.filter(p => p != cached)      // Remove the project from cache
            } catch (error) {
                console.error(error)
                throw error
            } finally {
                this.busy = false
            }
        },
        async getTaskByProjectIdAndNumber(projectId: string, taskNumber: number) {
            if (this.busy)
                return null;

            this.busy = true

            try {
                let cached = this.projects.find(p => p.id == projectId)
                if (!cached)
                    cached = await getProjectById(projectId)

                let task = cached.designTasks?.find(t => t.number == taskNumber)
                if (!task)
                {
                    task = await getTaskByProjectIdAndNumber(projectId, taskNumber)
                    cached.designTasks?.push(task)
                }

                return task;
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
                return addTask(projectId, {
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
        async updateTask(projectId: string, taskNumber: number, contract: UpdateDesignTaskDtoV1) {
            if (this.busy)
                return;

            this.busy = true

            try {
                const cached = this.projects.find(p => p.id === projectId)?.designTasks?.find(t => t.number === taskNumber)
                if (!cached)
                    throw new Error("Task could not be found.")

                await updateTask(cached.id, {
                    name: contract.name,
                    descriptionPayload: contract.descriptionPayload,
                    status: contract.status
                })

                cached.name = contract.name;
                cached.descriptionPayload = contract.descriptionPayload;
                cached.status = contract.status;
            } catch (error) {
                console.error(error)
                throw error
            } finally {
                this.busy = false
            }
        },
        async deleteTask(projectId: string, taskNumber: number) {
            if (this.busy)
                return;

            this.busy = true

            try {
                const cached = this.projects.find(p => p.id === projectId)?.designTasks?.find(t => t.number === taskNumber)
                if (!cached)
                    throw new Error("Task could not be found.")

                await deleteTask(cached?.id)

                if (cached)
                {
                    const project = this.projects.find(p => p.id === projectId);
                    if (project)
                        project.designTasks = project.designTasks?.filter(t => t.number != taskNumber)
                }
            } catch (error) {
                console.error(error)
                throw error
            } finally {
                this.busy = false
            }
        }
    },
})
