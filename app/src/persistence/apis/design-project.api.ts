import http from "@/http"
import type { AddDesignProjectDto, AddDesignProjectResultDto, DesignProjectDto } from "../dtos/design-project.dtos";

export async function addProject(contract: AddDesignProjectDto): Promise<AddDesignProjectResultDto> {
    const { data } = await http.post("/api/v1/app-design/projects/add", {
        name: contract.name,
        descriptionPayload: contract.descriptionPayload,
    })

    return data;
}

export async function getProjectById(projectId: string) {
    const { data } = await http.get<DesignProjectDto>(`/api/v1/app-design/projects/${projectId}?incltsk=true&incldsg=true`)
    return data;
}

export async function getOwnedProjects() {
    const { data } = await http.get<Array<DesignProjectDto>>("/api/v1/app-design/projects/owned?incltsk=true&incldsg=true")
    return data;
}
