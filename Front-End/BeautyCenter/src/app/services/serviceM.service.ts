import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Service } from '../_model/service';

import { Observable } from 'rxjs';
import { ServiceUser } from '../_model/service-user';
import { UserService2 } from '../_model/user-service2';

@Injectable({
  providedIn: 'root'
})
export class ServiceService {


  baseurl="https://localhost:7206/api/Services/"

  // baseurl="http://localhost:5240/api/Services"

  baseurl_Rana="http://localhost:5240/api/Services"

  constructor(public http:HttpClient) { }
  getall(){
    return this.http.get<Service[]>(this.baseurl);
  }
  getById(id: number): Observable<Service> {
    const url = `${this.baseurl}/${id}`;
    return this.http.get<Service>(url);
  }
  
  getAllCategories(){
    return this.http.get<string[]>(`${this.baseurl}AllCategories`)
  }

  getByCategory(category:string){
    return this.http.get<Service[]>(`${this.baseurl}ServicesByCategory/${category}`)
  }
}
