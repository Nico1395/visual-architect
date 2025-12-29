import { defineStore } from "pinia";
import type { DesignProjectDto } from "../dtos/design-project.dtos";
import { addProject, getProjects } from "../apis/design-project.api";

export const useDesignProjectStore = defineStore("design-project", {
    state: () => ({
        initialized: false,
        busy: false,
        projects: [] as Array<DesignProjectDto>,
    }),
    actions: {
        async getProjects() {
            if (this.busy)
                return;

            this.busy = true

            try {
                this.projects = await getProjects()
            } catch (error) {
                console.error(error)
            } finally {
                this.busy = false
            }
        },
        async addProject(name: string, description: string | null) {
            if (this.busy)
                return;

            this.busy = true

            try {
                await addProject({
                    name: name,
                    descriptionPayload: description,
                })
            } catch (error) {
                console.error(error)
            } finally {
                this.busy = false
            }
        }
    },
})
