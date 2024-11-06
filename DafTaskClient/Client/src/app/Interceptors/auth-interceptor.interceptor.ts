import { HttpRequest,  HttpHandlerFn } from '@angular/common/http';


export function AuthInterceptor (request: HttpRequest<any>, next: HttpHandlerFn){
    const token = sessionStorage.getItem('token');

    if (request.url.includes('/login') || request.url.includes('/register')) {
      return next(request);
    }

    if (token) {
      const cloned = request.clone({
        headers: request.headers.set('Authorization', `Bearer ${token}`)
      });
      return next(cloned);
    }

    return next(request);

    // return next(newRequest);
  }

