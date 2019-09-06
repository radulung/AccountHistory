import { HttpClient } from "@angular/common/http";
import { API_URL } from "../constants/constants";
import { Observable } from "rxjs/Observable";
import { Injectable } from "@angular/core";

@Injectable()
export class AccountHistoryApiService {

    constructor(private http: HttpClient) {

    }

    public getCurrentBalance(): Observable<Object> {
        return this.http.get(`${API_URL}/api/accountHistory/currentBalance`);
    }

    public getTransactionsByMonth(selectedMonth: number): Observable<Object> {
        return this.http.get(`${API_URL}/api/accountHistory/month/${selectedMonth}`);
    }

    public debit(amount: number): Observable<Object> {
        return this.http.post(`${API_URL}/api/accountHistory/debit`, { amount: amount });
    }

    public credit(amount: number): Observable<Object> {
        return this.http.post(`${API_URL}/api/accountHistory/credit`, { amount: amount });
    }
}