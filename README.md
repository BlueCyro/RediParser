# 📠 RediParser


This is a simple library to parse the REDIS protocol. It aims to provided a simple parser that gets the job done and leaves the network stack implementation up to the consumer.


# Why leave out networking?

Programs can have a wide variety of configurations and implementations for how their networking is handled. To best work with your needs, RediParser doesn't seek to impose its own ideas of network structure on your project.