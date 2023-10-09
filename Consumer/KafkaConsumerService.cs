using Confluent.Kafka;
using System.Diagnostics;
using System.Text.Json;

namespace Consumer
{
    public class KafkaConsumerService
    {
        private readonly IConsumer<string, string> _consumer;

        public KafkaConsumerService(IConfiguration configuration)
        {
            var kafkaConfig = new ConsumerConfig();
            configuration.GetSection("KafkaConfig").Bind(kafkaConfig);

            _consumer = new ConsumerBuilder<string, string>(kafkaConfig).Build();
            _consumer.Subscribe("my-topic-2");
        }

        public CarDetails Consume()
        {
            try
            {

                var consumeResult = _consumer.Consume();

                // Process the Kafka message
                var bookingRequest = JsonSerializer.Deserialize<CarDetails>(consumeResult.Message.Value);

                Debug.WriteLine($"Processing Order Id:  {bookingRequest.CarId} {bookingRequest.CarName} {bookingRequest.BookingStatus}");

                return bookingRequest;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
        
    }
}

