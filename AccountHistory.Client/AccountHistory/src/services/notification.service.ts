import { Injectable } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { ToastController } from 'ionic-angular';

@Injectable()
export class NotificationService {

    private _hubConnection: HubConnection;

    constructor(private toastCtrl: ToastController) {
        this.startNotificationListener();
        console.log('service instantiated');
    }

    private startNotificationListener(): void {
        this._hubConnection = new signalR.HubConnectionBuilder().withUrl('https://localhost:44379/notify').build();
        this._hubConnection
            .start()
            .then(() => console.log('Connection started!'))
            .catch(err => console.log('Error while establishing connection :('));

        this._hubConnection.onclose(() => console.log('close!!!'));
        this._hubConnection.on('BroadcastMessage', (message: string) => {
            const toast = this.toastCtrl.create({
                message: message,
                duration: 3000,
                position: 'top'
            });
            toast.present();
        });
    }
}