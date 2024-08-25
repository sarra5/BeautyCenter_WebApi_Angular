import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Package } from '../_model/package';

@Injectable({
  providedIn: 'root'
})
export class PackageService {

  constructor(public http:HttpClient) { }
  // Url="http://localhost:5240/api/Package/"
  Url="https://localhost:7206/api/Package/"

  GetAllPackage(){  //done
    return this.http.get<Package[]>(this.Url);
  }
  GetPackageByID(id:number){
    return this.http.get<Package>(this.Url+id)
  }
  DeletePackageById(id:number){ 
    return this.http.delete<"Deleted">(this.Url+id)
  }
  AddPackage(Newpackage:Package){  
    return this.http.post<"Added">(this.Url+"add",Newpackage)
  }
  EditPackage(EditedPackage:Package){
    return this.http.put<"edited">(this.Url+"edit",EditedPackage)
  }
}
