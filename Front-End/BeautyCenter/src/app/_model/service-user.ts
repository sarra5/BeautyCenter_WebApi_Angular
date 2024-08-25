import { Service } from "./service";

export class ServiceUser {
    constructor(public userId:number , public serviceId:number , public date:string ,public userName:string ,public serviceName:string, public serviceInfo :Service ){}
}
