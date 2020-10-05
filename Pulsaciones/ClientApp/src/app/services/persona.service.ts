import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { HandleHttpErrorService } from '../@base/handle-http-error.service';
import { Person } from '../models/person';
import { Observable} from 'rxjs';
import { tap, catchError } from 'rxjs/operators';



@Injectable({
  providedIn: 'root'
})
export class PersonaService {

  baseUrl: string;
  constructor(private http: HttpClient,@Inject('BASE_URL') baseUrl: string, private handleErrorService: HandleHttpErrorService)
  {
      this.baseUrl = baseUrl;
  }
  

  get(): Observable<Person[]> 
  {
    return this.http.get<Person[]>(this.baseUrl + 'api/Person').pipe(tap(_ => this.handleErrorService.log('datos enviados')),
      catchError(this.handleErrorService.handleError<Person[]>('Consultar Persona', null))
  );
  }

  post(person: Person): Observable<Person> {
    return this.http.post<Person>(this.baseUrl + 'api/Person',person)
           .pipe(tap(_ => this.handleErrorService.log('datos enviados')),
            catchError(this.handleErrorService.handleError<Person>('Registra Persona', null))
 );
 }

}
