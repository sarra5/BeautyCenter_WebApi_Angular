import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ServiceUser } from '../_model/service-user';
import { UserService2 } from '../_model/user-service2';

@Injectable({
  providedIn: 'root'
})
export class ServiceUserService {

  // baseUrl="http://localhost:5240/api/UserService";
  baseUrl="https://localhost:7206/api/UserService";

  baseUrlForGetByUserID="http://localhost:5240/api/UserService/by-user";

  constructor(public http: HttpClient) { }

  getall(): Observable<ServiceUser[]> {
    return this.http.get<ServiceUser[]>(this.baseUrl);
  }

  getByUserId(id: number): Observable<ServiceUser[]> {
    const url = `${this.baseUrlForGetByUserID}/${id}`;
    return this.http.get<ServiceUser[]>(url);
  }

  deleteById(userId: number, serviceId: number): Observable<any> {
    const url = `${this.baseUrl}/${userId}/${serviceId}`;
    console.log(`Deleting: ${url}`);
    return this.http.delete(url, { responseType: 'text' }).pipe(
      catchError(this.handleError)
    );
  }  

  deleteAllServiceInThisUser(userId: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${userId}`, { responseType: 'text' }).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    console.error('An error occurred:', error);
    return throwError('Something went wrong; please try again later.');
  }

  addServiceUser(userService:UserService2){
    return this.http.post<ServiceUser>(this.baseUrl,userService)
  }
}
