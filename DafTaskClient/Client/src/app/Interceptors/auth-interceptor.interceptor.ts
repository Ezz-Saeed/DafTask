import { HttpRequest,  HttpHandlerFn } from '@angular/common/http';


export function AuthInterceptor (request: HttpRequest<any>, next: HttpHandlerFn){
    const token = localStorage.getItem('token');

    if (request.url.includes('/login') || request.url.includes('/register')) {
      return next(request);
    }

    if (token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    }

    return next(request);
  }

