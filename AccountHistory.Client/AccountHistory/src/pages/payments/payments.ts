import { Component } from '@angular/core';
import { AccountHistoryApiService } from '../../services/account-history-api.service';

@Component({
  selector: 'page-payments',
  templateUrl: 'payments.html',
})
export class PaymentsPage {

  public amount: number;
  public description: string;

  constructor(private accountHistoryApiService: AccountHistoryApiService) {
  }

  public onBuyButtonPress() {
    this.accountHistoryApiService.debit(this.amount)
      .subscribe(data => {
        console.log('Debit success');
        this.amount = 0;
        this.description = '';
      }, error => {
        console.log(error);
      });
  }

  public onCreditButtonPress() {
    this.accountHistoryApiService.credit(this.amount)
      .subscribe(data => {
        console.log('Credit success');
        this.amount = 0;
        this.description = '';
      }, error => {
        console.log(error);
      });
  }
}
