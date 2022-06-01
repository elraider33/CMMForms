import { environment } from './../../../environments/environment';
import { HttpBackend, HttpClient, HttpEvent, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { timeout } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private httpBackend: HttpBackend) { }

  public get<T>(endpoint: string){
    return this.http.get<T>(`${this.baseUrl}api/${endpoint}`);
  }
  public getUserDocksiteInfo(token: string){
    var httpHeaders = new HttpHeaders();
    var httpClient = new HttpClient(this.httpBackend);
    httpHeaders.append('Authorization', `Bearer ${token}`);
    return httpClient.get(`${environment.authConfig.authorityUrl}/connect/userinfo`,{headers : httpHeaders});
  }
  public post(endpoint: string, data){
    return this.http.post(`${this.baseUrl}api/${endpoint}`, data);
  }

  public put(endpoint: string, data){
    return this.http.put(`${this.baseUrl}api/${endpoint}`, data);
  }

  public delete(endpoint: string){
    return this.http.delete(`${this.baseUrl}api/${endpoint}`);
  }

  public download(endpoint: string){
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/pdf');
    return this.http.get(
      `${this.baseUrl}api/${endpoint}`,
      {headers: headers, responseType: 'blob', reportProgress: true});
      // .pipe(timeout(900000));
  }
  public getDownloadUrl(endpoint){
    return `${this.baseUrl}api/${endpoint}`;
  }
  public downloadAlternative(id: string){
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/pdf');
    const url = `http://ec2-3-92-156-177.compute-1.amazonaws.com/gridfs/fs/key/${id}?download=true`
    return this.http.get(
      url,
      {headers: headers, responseType: 'blob', reportProgress: true})
      .pipe(timeout(900000));
  }
}
