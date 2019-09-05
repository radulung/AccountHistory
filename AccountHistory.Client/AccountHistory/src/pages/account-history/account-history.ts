import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '../../constants/constants';
import { MonthModel } from '../../models/month.model';

@Component({
  selector: 'page-account-history',
  templateUrl: 'account-history.html',
})
export class AccountHistoryPage {

  public currentBalance: number = 0;
  public selectedMonth: number;
  public months = [
    { id: 1, name: 'January' },
    { id: 2, name: 'February' },
    { id: 3, name: 'March' },
    { id: 4, name: 'April' },
    { id: 5, name: 'May' },
    { id: 6, name: 'June' },
    { id: 7, name: 'July' },
    { id: 8, name: 'August' },
    { id: 9, name: 'September' },
    { id: 10, name: 'Octomber' },
    { id: 11, name: 'November' },
    { id: 12, name: 'December' }
  ];

  constructor(private http: HttpClient) {
  }

  public ionViewWillEnter() {
    this.fillMonths();
    this.getCurrentBalance();
  }

  private fillMonths(): void {
  }

  private getCurrentBalance(): void {
    this.http.get(`${API_URL}api/accountHistory/currentBalance`)
      .subscribe((data: number) => {
        this.currentBalance = data;
      }, error => {
        console.log(error);
      });
  }

  public getTransactionsByMonth(): void {
    this.http.get(`${API_URL}api/accountHistory/month/${this.selectedMonth}`)
      .subscribe((data) => {
        console.log(data);
      }, error => {
        console.log(error);
      });
  }
}
