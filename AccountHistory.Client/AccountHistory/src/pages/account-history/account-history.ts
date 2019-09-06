import { Component } from '@angular/core';
import { AccountHistoryApiService } from '../../services/account-history-api.service';
import { TransactionDto } from '../../dtos/transaction.dto';

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
  public transactions: TransactionDto[];

  constructor(private accountHistoryApiService: AccountHistoryApiService) {
  }

  public ionViewWillEnter() {
    this.fillMonths();
    this.getCurrentBalance();
  }

  private fillMonths(): void {
  }

  private getCurrentBalance(): void {
    this.accountHistoryApiService.getCurrentBalance()
      .subscribe((currentBalance: number) => {
        this.currentBalance = currentBalance;
      }, error => {
        console.log(error);
      });
  }

  public getTransactionsByMonth(): void {
    this.accountHistoryApiService.getTransactionsByMonth(this.selectedMonth)
      .subscribe((transactions: TransactionDto[]) => {
        this.transactions = transactions;
      }, error => {
        console.log(error);
      });
  }
}
