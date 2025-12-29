import http from "@/http"
import type { AddDesignProjectDto, DesignProjectDto } from "../dtos/design-project.dtos";

export async function getProjects() {
    const { data } = await http.get<Array<DesignProjectDto>>("/api/v1/app-design/projects");
    return data
}

export async function addProject(contract: AddDesignProjectDto) {
    await http.post("/api/v1/app-design/projects/add", {
        name: contract.name,
        descriptionPayload: contract.descriptionPayload,
    })
}
