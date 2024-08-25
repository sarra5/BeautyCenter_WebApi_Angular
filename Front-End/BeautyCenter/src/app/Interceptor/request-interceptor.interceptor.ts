import { HttpInterceptorFn } from '@angular/common/http';

export const requestInterceptorInterceptor: HttpInterceptorFn = (req, next) => {
  let t = localStorage.getItem("token")
  if(t == null){
    return next(req);
  }
  req=req.clone({
    setHeaders:{
      Authorization:`Bearer ${t}`
    }
  })
  return next(req)
};
