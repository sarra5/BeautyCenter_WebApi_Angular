import { Package } from "./package";

export class PackageUser {
    constructor(public userId :number , public packageId:number , public date:string , public packageName :string , public userName:string ,  public packagInfo :Package){}
}
