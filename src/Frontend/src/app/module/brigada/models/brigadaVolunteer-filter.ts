export interface BrigadeVolunteerFilter {
    id?: number | string;
    nombreBrigada ?: string;
    responsableBrigada ?: string;
    nombreVoluntario ?: string;
    brigadeId?: number | string;
    volunteerId ?: number | string;
    offset: number,
    take: number,
    sort: string,



   
}
