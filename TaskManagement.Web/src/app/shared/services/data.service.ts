import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, throwError } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  baseUrl:string = environment.apiBaseUrl;
    /**
     * Constructor.
     */
    constructor(private http: HttpClient) {

    }

    handleError(error: Response) {
        return throwError(() => error.statusText);
    }

    /**
    * get all by get.
    */
    public getAllByGet<t>(actionUrl: any, param?: any, authUrl?: string): Observable<t> {
        const endpointUrl: string = this.baseUrl + actionUrl;
        return this.http.get<t>(endpointUrl, { params: param })
            .pipe(
                map((response: t) => { return response; }),
                catchError(this.handleError)
            );
    }

    ///**
    // * Get all by reference data by post.
    // */
    public getAllByGetWithJson<T>(actionurl: any, param?: any): Observable<T> {
        return this.http.get<T>(actionurl + JSON.stringify(param))
            .pipe(
                map((response: T) => { return response; }),
                catchError(this.handleError)
            );

    }

    ///**
    // * Get all by Post.
    // */
    public getAllByPost<T>(actionUrl: any, params: any, authUrl?: string): Observable<T> {
        const endpointUrl: string = this.baseUrl + actionUrl;

        return this.http.post<T>(endpointUrl, params)
            .pipe(
                map((response: T) => { return response; }),
                catchError(this.handleError)
            );
    }

    /**
   * Add.
   */
    public add<T>(actionUrl: any, object: any): Observable<T> {
        const endpointUrl: string = this.baseUrl + actionUrl;
        return this.http.post<T>(endpointUrl, object)
            .pipe(
                map((response: T) => { return response; }),
                catchError(this.handleError)
            );
    }

    /**
    * Put.
    */
    public put<T>(actionUrl: any, object: any): Observable<T> {
        const endpointUrl: string = this.baseUrl + actionUrl;
        return this.http.put<T>(endpointUrl+"/"+object.id, object)
            .pipe(
                map((response: T) => { return response; }),
                catchError(this.handleError)
            );
    }

    /**
    * Delete.
    */
    public delete<T>(actionUrl: any, object: any): Observable<T> {
        const endpointUrl: string = this.baseUrl + actionUrl;

        return this.http.delete<T>(endpointUrl+"/"+object)
            .pipe(
                map((response: T) => { return response; }),
                catchError(this.handleError)
            );
    }
}
