import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";

@Injectable()
export class BasicAuthInterceptor implements HttpInterceptor {

  constructor() { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    let basicAuthUserName = environment.basicAuthUserName;
    let basicAuthPassword = environment.basicAuthPassword;

    const basicAuth = 'Basic ' + btoa(`${basicAuthUserName}:${basicAuthPassword}`);

    const authReq = request.clone({
      setHeaders: {
        Authorization: basicAuth
      }
    });
    return next.handle(authReq);

  }
}

