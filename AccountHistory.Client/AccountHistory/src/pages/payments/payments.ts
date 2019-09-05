import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '../../constants/constants';

@Component({
  selector: 'page-payments',
  templateUrl: 'payments.html',
})
export class PaymentsPage {

  public amount: number;
  public description: string;

  constructor(private http: HttpClient) {
  }

  public ionViewDidLoad() {
    console.log('ionViewDidLoad PaymentsPage');
  }

  public onBuyButtonPress() {
    this.http.post(`${API_URL}api/accountHistory/debit`, { amount: this.amount })
      .subscribe(data => {
        console.log('Debit success');
        this.amount = 0;
        this.description = '';
      }, error => {
        console.log(error);
      });
  }

  public onCreditButtonPress() {
    this.http.post(`${API_URL}api/accountHistory/credit`, { amount: this.amount })
      .subscribe(data => {
        console.log('Credit success');
        this.amount = 0;
        this.description = '';
      }, error => {
        console.log(error);
      });
  }

}
